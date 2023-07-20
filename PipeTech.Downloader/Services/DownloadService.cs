// <copyright file="DownloadService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NASSCO_ExV;
using Nito.AsyncEx;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.Models;
using PT.Inspection;
using PT.Inspection.Inspections;
using PT.Inspection.Model;
using PT.Inspection.Packs;
using PT.Inspection.PipeTech.DataConversion;
using PT.Inspection.Reporting;
using PT.Inspection.Templates;
using PT.Inspection.Wpf;
using PT.Model;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Download service class.
/// </summary>
public class DownloadService : ObservableObject, IDownloadService
{
    /// <summary>
    /// Part minimum size.
    /// </summary>
    private static readonly SemaphoreSlim Semaphore = new(1);
    private static readonly SemaphoreSlim ProjectWritingSemaphore = new(1);

    private readonly IServiceProvider serviceProvider;
    private readonly IBackgroundJobClientV2 jobClient;
    private readonly SettingsDirectoryPaths paths;
    private readonly ILogger<DownloadService>? logger;
    private readonly DownloadSettingsOptions downloadSettings;
    private readonly IJsonDeserializerV2 jsonDeserializer;
    private readonly IReportRegistry reportRegistry;
    private readonly IInspectionFactory inspectionFactory;
    private readonly ITemplateRegistry templateRegistry;
    private readonly IDocumentFactory documentFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadService"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    /// <param name="jobClient">Job client.</param>
    /// <param name="options">Settings path options.</param>
    /// <param name="downloadOptions">Download options.</param>
    /// <param name="jsonDeserializer">JSON deserializer service.</param>
    /// <param name="reportRegistry">Report Registry service.</param>
    /// <param name="inspectionFactory">Inspection factory service.</param>
    /// <param name="templateRegistry">Template registry service.</param>
    /// <param name="documentFactory">Document factory service.</param>
    /// <param name="logger">Logger service.</param>
    public DownloadService(
        IServiceProvider serviceProvider,
        IBackgroundJobClientV2 jobClient,
        IOptions<SettingsDirectoryPaths> options,
        IOptions<DownloadSettingsOptions> downloadOptions,
        IJsonDeserializerV2 jsonDeserializer,
        IReportRegistry reportRegistry,
        IInspectionFactory inspectionFactory,
        ITemplateRegistry templateRegistry,
        IDocumentFactory documentFactory,
        ILogger<DownloadService>? logger = null)
    {
        this.serviceProvider = serviceProvider;
        this.jobClient = jobClient;
        this.paths = options.Value;
        this.downloadSettings = downloadOptions?.Value ?? new();
        this.jsonDeserializer = jsonDeserializer;
        this.reportRegistry = reportRegistry;
        this.inspectionFactory = inspectionFactory;
        this.templateRegistry = templateRegistry;
        this.documentFactory = documentFactory;
        this.logger = logger;

        this.Source = new();
        this.Source.CollectionChanged += this.Source_CollectionChanged;

        _ = this.LoadDownloads(new CancellationTokenSource(TimeSpan.FromMinutes(10)).Token);

#if DEBUG
        try
        {
            this.jobClient.Enqueue<IDownloadService>(s => s.Test(CancellationToken.None));
        }
        catch (Exception)
        {
        }
#endif
    }

    /// <inheritdoc/>
    public ObservableCollection<Project> Source
    {
        get;
    }

    /// <inheritdoc/>
    public async Task LoadDownloads(CancellationToken token = default)
    {
        if (string.IsNullOrEmpty(this.paths.MachineSettingsDirectory) ||
            !Directory.Exists(this.paths.MachineSettingsDirectory))
        {
            return;
        }

        await Task.Run(
            async () =>
            {
                var release = false;
                try
                {
                    await Semaphore.WaitAsync(token);
                    release = true;
                    foreach (var dir in Directory.GetDirectories(this.paths.MachineSettingsDirectory))
                    {
                        if (string.IsNullOrEmpty(dir))
                        {
                            continue;
                        }

                        try
                        {
                            var infoFilePath = Path.Combine(dir, "info.json");
                            if (!System.IO.File.Exists(infoFilePath))
                            {
                                continue;
                            }

                            var project = Project.FromJson(System.IO.File.ReadAllText(infoFilePath));
                            if (project is null)
                            {
                                this.logger?.LogWarning($"Info about download unable to be parsed. [{infoFilePath}]");
                                continue;
                            }

                            project.FilePath = infoFilePath;

                            if (!this.Source.Any(p => p.Id == project.Id))
                            {
                                var count = 0;
                                while (count <= 1000)
                                {
                                    count++;
                                    bool AddRemove()
                                    {
                                        try
                                        {
                                            this.Source.Insert(0, project);
                                            return true;
                                        }
                                        catch (Exception)
                                        {
                                            try
                                            {
                                                this.Source.Remove(project);
                                            }
                                            catch (Exception)
                                            {
                                            }
                                        }

                                        return false;
                                    }

                                    if (App.MainWindow.DispatcherQueue.HasThreadAccess)
                                    {
                                        if (AddRemove())
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        var b = await App.MainWindow.DispatcherQueue.EnqueueAsync(
                                            () =>
                                            {
                                                return AddRemove();
                                            },
                                            Microsoft.UI.Dispatching.DispatcherQueuePriority.Low);

                                        if (b)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            this.logger?.LogError(ex, $"Error getting download at path [{dir}]");
                        }

                        if (token.IsCancellationRequested)
                        {
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger?.LogError(ex, $"Error getting download");
                }
                finally
                {
                    if (release)
                    {
                        Semaphore.Release();
                    }
                }
            },
            token);
    }

    /// <inheritdoc/>
    public IEnumerable<KeyValuePair<string, Job>> FindJobForProject(Project project)
    {
        var dict = new Dictionary<string, Job>();
        var monitor = this.jobClient.Storage.GetMonitoringApi();
        try
        {
            // ASSUMPTION: We are going to assume that the number of
            // jobs will NEVER exceed an integer. (2,147,483,647)
            var jobs = monitor.EnqueuedJobs(
                "default",
                0,
                (int)Math.Min(int.MaxValue, monitor.EnqueuedCount("default")))
                .Where(j => j.Value.Job.Type == typeof(IDownloadService) &&
                j.Value.Job.Method.Name == nameof(this.DownloadProject) &&
                j.Value.Job.Args.Count >= 1 &&
                j.Value.Job.Args[0] is string path &&
                path.ToUpperInvariant() == project.FilePath?.ToUpperInvariant());
            foreach (var j in jobs)
            {
                dict.Add(j.Key, j.Value.Job);
            }

            var processing = monitor.ProcessingJobs(
                0,
                (int)Math.Min(int.MaxValue, monitor.ProcessingCount()))
                .Where(j => j.Value.Job.Type == typeof(IDownloadService) &&
                j.Value.Job.Method.Name == nameof(this.DownloadProject) &&
                j.Value.Job.Args.Count >= 1 &&
                j.Value.Job.Args[0] is string path &&
                path.ToUpperInvariant() == project.FilePath?.ToUpperInvariant());
            foreach (var j in processing)
            {
                dict.Add(j.Key, j.Value.Job);
            }

            var scheduled = monitor.ScheduledJobs(
                0,
                (int)Math.Min(int.MaxValue, monitor.ScheduledCount()))
                .Where(j => j.Value.Job.Type == typeof(IDownloadService) &&
                j.Value.Job.Method.Name == nameof(this.DownloadProject) &&
                j.Value.Job.Args.Count >= 1 &&
                j.Value.Job.Args[0] is string path &&
                path.ToUpperInvariant() == project.FilePath?.ToUpperInvariant());
            foreach (var j in scheduled)
            {
                dict.Add(j.Key, j.Value.Job);
            }
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Error finding jobs for project [{project.ToJson()}]");
        }
        finally
        {
            if (monitor is IDisposable d)
            {
                d.Dispose();
            }
        }

        return dict;
    }

    /// <inheritdoc/>
    public async Task CreateJobForProject(Project project)
    {
        if (project.Inspections is not null)
        {
            foreach (var inspection in project.Inspections)
            {
                var path = inspection.Inspection?.DownloadPath;
                if (string.IsNullOrEmpty(path))
                {
                    var message = $"An inspection in project [{project.FilePath}] is empty.";
                    this.logger?.LogWarning(message);
                    if (inspection.Inspection is not null)
                    {
                        if (App.MainWindow.DispatcherQueue.HasThreadAccess)
                        {
                            inspection.Inspection.State = DownloadInspection.States.Errored;
                            inspection.Inspection.LastError = $"{message}";
                        }
                        else
                        {
                            await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                             {
                                 inspection.Inspection.State = DownloadInspection.States.Errored;
                                 inspection.Inspection.LastError = $"{message}";
                             });
                        }
                    }

                    continue;
                }

                if (inspection.Inspection is not null)
                {
                    if (App.MainWindow.DispatcherQueue.HasThreadAccess)
                    {
                        inspection.Inspection.State = DownloadInspection.States.Queued;
                    }
                    else
                    {
                        await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                        {
                            inspection.Inspection.State = DownloadInspection.States.Queued;
                        });
                    }
                }
            }
        }

        project.WasCompleted = null;
        if (App.MainWindow.DispatcherQueue.HasThreadAccess)
        {
            project.RaiseStateChanged();
        }
        else
        {
            await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
            {
                project.RaiseStateChanged();
            });
        }

        await this.WriteProject(project);
        this.jobClient.Enqueue<IDownloadService>(
            s => s.DownloadProject(
                project.FilePath!,
                CancellationToken.None));
    }

    /// <inheritdoc/>
    public async Task DownloadProject(
        string projectFilePath,
        CancellationToken token)
    {
        var dq = App.MainWindow.DispatcherQueue;

        var project = default(Project);
        var compositeExchangeDBPath = default(string);
        var compositeReportPath = default(string);
        try
        {
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            project = this.Source.FirstOrDefault(p => p.FilePath?.ToUpperInvariant() == projectFilePath.ToUpperInvariant());
            if (project is null)
            {
                this.logger?.LogWarning($"Project [{projectFilePath}] no longer exists.");
                return;
            }

            var inspectionErrors = new ConcurrentBag<Exception>();
            var firstPack = default(IPack);
            var inspectionType = default(InspectionTable);
            compositeExchangeDBPath = project.GetExchangeDBPath();
            compositeReportPath = project.GetCombinedReportPath();

            if (project.Inspections?.Any() == true)
            {
                await Parallel.ForEachAsync(
                    project.Inspections,
                    new ParallelOptions()
                    {
                        CancellationToken = token,
#if DEBUG
                        MaxDegreeOfParallelism = 1,
#else
                        MaxDegreeOfParallelism = 1,
#endif
                    },
                    async (handler, token) =>
                    {
#if SLOWDOWN
                        await Task.Delay(10000, token);
#endif

                        var inspection = default(DownloadInspection);
                        var inspectionObject = default(Inspection);
                        try
                        {
                            var inspectionPath = handler.Inspection?.DownloadPath;
                            ////var handler = project.Inspections?
                            ////    .FirstOrDefault(dlh => dlh.Inspection?.DownloadPath?.ToUpperInvariant() == inspectionPath.ToUpperInvariant());
                            inspection = handler.Inspection;

                            if (inspection is null || inspection.State == Models.DownloadInspection.States.Loading)
                            {
                                await Task.Delay(1000, token);
                                await handler!.LoadInspection(token);
                                inspection = handler.Inspection;
                            }

                            if (inspection is null ||
                            inspectionPath is null ||
                            string.IsNullOrEmpty(inspectionPath))
                            {
                                this.logger?.LogWarning($"Inspection [{inspectionPath}] in project [{projectFilePath}] no longer exists.");
                                return;
                            }

                            if (token.IsCancellationRequested)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Paused;
                                    inspection.LastError = $"Inspection paused";
                                });
                                await this.WriteProject(project);
                                return;
                            }

                            switch (inspection.State)
                            {
                                case DownloadInspection.States.Loading:
                                    // Still loading. Can't do anything about it
                                    throw new InvalidDataException($"Inspection [{inspectionPath}] in project [{projectFilePath}] is still loading. Unable to process.");
                                case DownloadInspection.States.Errored:
                                    throw new InvalidDataException($"Inspection [{inspectionPath}] in project [{projectFilePath}] is in the error state. Unable to process.");
                                case Models.DownloadInspection.States.Complete:
                                    this.logger?.LogInformation($"Inspection [{inspectionPath}] in project [{projectFilePath}] is complete. No need to process.");
                                    return;
                                case DownloadInspection.States.Queued:
                                case DownloadInspection.States.Staged:
                                case DownloadInspection.States.Processing:
                                case DownloadInspection.States.Paused:
                                    // All these states are ok to proceed and check the files.
                                    break;
                                default:
                                    break;
                            }

                            if (token.IsCancellationRequested)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Paused;
                                    inspection.LastError = $"Inspection paused";
                                });
                                await this.WriteProject(project);
                                return;
                            }

                            // Change the state to processing.
                            await dq.EnqueueAsync(() =>
                            {
                                inspection.State = DownloadInspection.States.Processing;
                            });
                            await this.WriteProject(project);

                            if (inspectionPath is not null && !Directory.Exists(inspectionPath))
                            {
                                Directory.CreateDirectory(inspectionPath);
                            }

                            if (project.CombinedNASSCOExchangeGenerate == true && System.IO.File.Exists(compositeExchangeDBPath))
                            {
                                System.IO.File.Delete(compositeExchangeDBPath);
                                await dq.EnqueueAsync(() =>
                                {
                                    project?.RaiseProgressChanged();
                                });
                            }

                            if (project.CombinedReportIds?.Any() == true && System.IO.File.Exists(compositeReportPath))
                            {
                                System.IO.File.Delete(compositeReportPath);
                                await dq.EnqueueAsync(() =>
                                {
                                    project?.RaiseProgressChanged();
                                });
                            }

                            // Let's do each file
                            var states = new ConcurrentBag<DownloadInspection.States>();
                            var errors = new ConcurrentBag<Exception>();
                            if (inspection.Files is not null)
                            {
                                await Parallel.ForEachAsync(
                                    inspection.Files.Where(f => f.Name?.ToUpperInvariant().EndsWith(".PTDX") == false),
                                    new ParallelOptions()
                                    {
                                        CancellationToken = token,
#if DEBUG
                                        MaxDegreeOfParallelism = 1,
#else
                                        MaxDegreeOfParallelism = 5,
#endif
                                    },
                                    async (file, token) =>
                                    {
#if SLOWDOWN
                                        await Task.Delay(10000, token);
#endif

                                        try
                                        {
                                            if (token.IsCancellationRequested)
                                            {
                                                states.Add(DownloadInspection.States.Paused);
                                                return;
                                            }

                                            if (!Uri.TryCreate(file.Name, UriKind.RelativeOrAbsolute, out var uri))
                                            {
                                                throw new Exception($"Invalid file uri [{file.Name}]");
                                            }

                                            if (!uri.IsAbsoluteUri)
                                            {
                                                throw new Exception($"Invalid file uri type [{file.Name}]");
                                            }

                                            if (string.IsNullOrEmpty(file.DownloadPath))
                                            {
                                                throw new Exception($"Invalid download path for file [{file.DownloadPath}]");
                                            }

                                            var destinationFile = default(FileInfo);
                                            if (Path.IsPathRooted(file.DownloadPath))
                                            {
                                                destinationFile = new FileInfo(file.DownloadPath);
                                            }
                                            else
                                            {
                                                destinationFile = new FileInfo(Path.Combine(inspectionPath!, file.DownloadPath));
                                            }

                                            if (uri.IsFile)
                                            {
                                                var sourceFile = new FileInfo(uri.LocalPath);

                                                if (!sourceFile.Exists)
                                                {
                                                    throw new Exception($"Source file [{sourceFile.FullName}] does not exist.");
                                                }

                                                if (!destinationFile.Exists ||
                                                destinationFile.MD5FileBase64Hash() != sourceFile.MD5FileBase64Hash())
                                                {
                                                    // Copy
                                                    sourceFile.CopyTo(destinationFile.FullName);
                                                }
                                            }
                                            else if (uri.Scheme.ToUpperInvariant() == "HTTP" ||
                                            uri.Scheme.ToUpperInvariant() == "HTTPS")
                                            {
#if SLOWDOWN
                                                await Task.Delay(10000, token);
#endif

                                                var (size, acceptsRange, etag) = await this.GetUriInfo(uri);

                                                if (token.IsCancellationRequested)
                                                {
                                                    states.Add(Models.DownloadInspection.States.Paused);
                                                    return;
                                                }

                                                await dq.EnqueueAsync(() =>
                                                {
                                                    if (size is not null)
                                                    {
                                                        file.Size = size;
                                                    }

                                                    file.DownloadedSize = destinationFile.Exists ? destinationFile.Length : 0L;
                                                });

                                                if (!destinationFile.Exists ||
                                                destinationFile.Length != size) // ||
                                                                                ////destinationFile.MD5FileHexHash() != etag.Replace("-", string.Empty).Replace("\"", string.Empty))
                                                {
                                                    using var httpClient = this.serviceProvider.GetService(typeof(HttpClient)) as HttpClient;

                                                    if (httpClient is null)
                                                    {
                                                        throw new NullReferenceException("Unable to create http client.");
                                                    }

                                                    if (size is null || size <= HttpHelper.MINSIZE || !acceptsRange)
                                                    {
#if SLOWDOWN
                                                        await Task.Delay(10000, token);
#endif

                                                        // Do it in one part
                                                        var response = await httpClient.SendAsync(
                                                            new HttpRequestMessage(HttpMethod.Get, uri));
                                                        response.EnsureSuccessStatusCode();
                                                        using (var ms = new MemoryStream())
                                                        {
                                                            response.Content.ReadAsStream().CopyTo(ms);

                                                            ms.Seek(0, SeekOrigin.Begin);

                                                            using var fs = new FileStream(
                                                                destinationFile.FullName,
                                                                FileMode.Create,
                                                                FileAccess.Write);
                                                            ms.CopyTo(fs);
                                                        }

                                                        destinationFile.Refresh();

                                                        await dq.EnqueueAsync(() =>
                                                        {
                                                            file.DownloadedSize = destinationFile.Length;
                                                        });
                                                    }
                                                    else
                                                    {
                                                        // Break it up
                                                        // Resume based on what's already downloaded.
                                                        var byteCount = destinationFile.Exists ? destinationFile.Length : 0L;
                                                        while (byteCount < size)
                                                        {
                                                            GC.Collect();

                                                            if (token.IsCancellationRequested)
                                                            {
                                                                states.Add(Models.DownloadInspection.States.Paused);
                                                                return;
                                                            }

#if SLOWDOWN
                                                            await Task.Delay(10000, token);
#endif

                                                            var workingSize = Math.Min(
                                                                HttpHelper.MINSIZE,
                                                                size.Value - byteCount);

                                                            var request = new HttpRequestMessage(HttpMethod.Get, uri);
                                                            request.Headers.Range =
                                                            new System.Net.Http.Headers.RangeHeaderValue(
                                                                byteCount, byteCount + (workingSize - 1));
                                                            var response = await httpClient.SendAsync(request);
                                                            response.EnsureSuccessStatusCode();
                                                            using (var ms = new MemoryStream())
                                                            {
                                                                response.Content.ReadAsStream().CopyTo(ms);

                                                                ms.Seek(0, SeekOrigin.Begin);

                                                                using var fs = new FileStream(
                                                                    destinationFile.FullName,
                                                                    FileMode.Append,
                                                                    FileAccess.Write);
                                                                ms.CopyTo(fs);
                                                            }

                                                            byteCount += workingSize;
                                                            destinationFile.Refresh();

                                                            await dq.EnqueueAsync(() =>
                                                            {
                                                                file.DownloadedSize = destinationFile.Length;
                                                            });
                                                        }
                                                    }
                                                }

                                                GC.Collect();
                                            }

                                            states.Add(Models.DownloadInspection.States.Complete);
                                        }
                                        catch (Exception ex)
                                        {
                                            states.Add(Models.DownloadInspection.States.Errored);
                                            errors.Add(ex);
                                            return;
                                        }
                                    });
                            }

                            if (token.IsCancellationRequested)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Paused;
                                    inspection.LastError = $"Inspection paused";
                                });
                                await this.WriteProject(project);
                                return;
                            }

                            // Unless we got all the media and attachments, then making data is worthless.
                            // So there is no need to write the data, base.pack, reports, and exchange dbs.
                            if (states.Any(s => s == DownloadInspection.States.Errored))
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = $"Inspection errored\r\n\r\nError details:\r\n{string.Join("\r\n", errors.Where(e => e is not null))}";
                                });
                                throw new AggregateException(errors.ToArray());
                            }
                            else if (states.Any(s => s == DownloadInspection.States.Paused))
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Paused;
                                    inspection.LastError = $"Inspection paused";
                                });
                                throw new TaskCanceledException();
                            }

                            // Continue with writing the data, base.pack, making reports, and exchange dbs
                            var packId = handler.PackId ?? Guid.Empty;
                            if (packId == Guid.Empty)
                            {
                                using var dsTemp = this.jsonDeserializer.Deserialize(inspection.Json?.ToString() ?? string.Empty);
                                if (dsTemp is null)
                                {
                                    var message = $"Unable to parse inspection information [{inspectionPath}] in project [{projectFilePath}]. Unable to process.";
                                    await dq.EnqueueAsync(() =>
                                    {
                                        inspection.State = DownloadInspection.States.Errored;
                                        inspection.LastError = message;
                                    });
                                    throw new InvalidDataException(message);
                                }

                                packId = dsTemp.GetPackID() ?? Guid.Empty;
                            }

                            var pack = this.templateRegistry.GetTemplate(packId);
                            if (pack is null)
                            {
                                var message = $"Invalid template pack in [{inspectionPath}] in project [{projectFilePath}]. Unable to process.";
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = message;
                                });
                                throw new NullReferenceException(message);
                            }

                            firstPack ??= pack;

                            using var ds = this.jsonDeserializer.Deserialize(pack, inspection.Json?.ToString() ?? string.Empty);

                            if (!ds.TryGetInspectionRow(out var inspectionRow) || inspectionRow is null)
                            {
                                var message = $"No inspection data in [{inspectionPath}] in project [{projectFilePath}]. Unable to process.";
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = message;
                                });
                                throw new DataException(message);
                            }

                            inspectionType = InspectionTable.FromString(inspectionRow.Table.TableName);

                            var assetRow = inspectionRow.GetParentRow(inspectionRow.Table.GetParentRelationByPrefix(TablePrefixes.Asset));
                            if (assetRow is null)
                            {
                                var message = $"No asset data in [{inspectionPath}] in project [{projectFilePath}]. Unable to process.";
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = message;
                                });
                                throw new DataException(message);
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspectionObject = this.inspectionFactory.InitializeInspection(
                                pack,
                                ds,
                                inspectionRow.Table.TableName) as Inspection;
                            });

                            if (inspectionObject is null)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = "Unable to create inspection for saving";
                                });
                                throw new TypeInitializationException(typeof(Inspection).FullName, null);
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspectionObject.AssetCollection.View.MoveCurrentToFirst();
                                inspectionObject.InspectionCollection.View.MoveCurrentToFirst();
                                inspectionObject.FileName = Path.Combine(
                                    inspectionPath!,
                                    inspectionObject.ResolveInspectionFileNameWithoutExtension() + ".ptdx");

                                var currentOk = inspectionObject.InspectionObservationFocusCollection.View.MoveCurrentToFirst();
                                while (currentOk)
                                {
                                    var of = inspectionObject.CurrentInspectionObservationFocusRowView;
                                    var mp = inspectionObject.GetFieldRawValue(of, of.Row.Table.TableName, "Media_Parameters");
                                    if (mp is string mpString && mpString.EndsWith("t") &&
                                    double.TryParse(mpString.TrimEnd('t'), out var time))
                                    {
                                        var ts = TimeSpan.FromMilliseconds(time);
                                        of.SetColumnValue(
                                            "Media_Parameters",
                                            ts.TotalMilliseconds.ToString().Encrypt(inspectionObject.GetInspectionGuid().ToString()));
                                    }

                                    currentOk = inspectionObject.InspectionObservationFocusCollection.View.MoveCurrentToNext();
                                }

                                // Change the media paths to local-relative
                                foreach (var mediaReference in inspectionObject.MediaReferences.ToArray())
                                {
                                    inspectionObject.MediaReferences.Replace(
                                        mediaReference,
                                        mediaReference.UpdatePath(
                                            new Uri(
                                                Uri.UnescapeDataString(Path.GetFileName(mediaReference.URI.OriginalString)),
                                                UriKind.Relative)));
                                }

                                // This writes the base.pack and .ptdx
                                inspectionObject.Save(inspectionObject.FileName);
                                inspection.DataCompletePath = inspectionObject.FileName;
                            });

                            var reportPath = Path.Combine(
                                inspectionPath!,
                                inspectionObject!.GetDefaultPdfFilenameOnly());

                            var document = default(IDocument);
                            try
                            {
                                // Reports
                                if (project.IndividualReportIds?.Any() == true)
                                {
                                    document = this.documentFactory.GetDocument();
                                    document.Author = "Industrial Technology Group";
                                    document.Culture = inspectionObject?.SourcePack?.Metadata.DeliveryCulture ?? CultureInfo.CurrentUICulture;
                                    foreach (var reportId in project.IndividualReportIds)
                                    {
                                        var report = this.reportRegistry.GetReport<IInspectionReportDefinition>(reportId);
                                        if (report is null)
                                        {
                                            continue;
                                        }

                                        report.Inspection = inspectionObject;

                                        if (report is ILetterheadAware letterheadAware)
                                        {
                                            var inspectionLetterhead = inspection as ILetterheadAware;
                                            var templatePack = this.templateRegistry?.GetTemplate(
                                                inspectionObject!.SourcePack?.Metadata?.ID ?? Guid.Empty);

                                            var letterhead = inspectionLetterhead?.Letterhead ??
                                                inspectionObject!
                                                .SourcePack?.GetExternalReportLetterheadDefinition() ??
                                                templatePack?.GetExternalReportLetterheadDefinition() ??
                                                inspectionObject.SourcePack?
                                                    .GetReportLetterheadDefinition(inspectionObject.InspectionTableName) ??
                                                    templatePack?
                                                    .GetReportLetterheadDefinition(inspectionObject.InspectionTableName);

                                            if (letterhead is not null)
                                            {
                                                letterheadAware.Letterhead = letterhead;
                                            }
                                        }

                                        if (report is ICanRenderSalesWatermark sales)
                                        {
                                            sales.ShouldRenderSalesWatermark = false;
                                        }

                                        if (report is ICanRenderSupportAndTestingWatermark support)
                                        {
                                            support.ShouldRenderSupportAndTestingWatermark = false;
                                        }

                                        if (report is ICanRenderEnumeratedWatermark watermark)
                                        {
                                            watermark.Watermark = ICanRenderEnumeratedWatermark.Watermarks.None;
                                        }

                                        document.AddReport(report);
                                    }

#if SLOWDOWN
                                    await Task.Delay(10000, token);
#endif

                                    using (var fs = new FileStream(reportPath, FileMode.Create))
                                    {
                                        await document.Generate(fs);
                                    }

                                    if (document is IDisposable dis)
                                    {
                                        dis.Dispose();
                                    }

                                    await dq.EnqueueAsync(() =>
                                    {
                                        inspection.ReportCompletePath = reportPath;
                                    });
                                }
                            }
                            finally
                            {
                                if (document is IDisposable d)
                                {
                                    d.Dispose();
                                }

                                GC.Collect();
                            }

                            try
                            {
                                if (project.IndividualNASSCOExchangeGenerate == true ||
                                project.CombinedNASSCOExchangeGenerate == true)
                                {
                                    // Validate
                                    var validate = default(IValidation);
                                    var logicOverride = default(Utility.GetLogicOverrideDelegate);
                                    var culture = inspectionObject?.SourcePack?.Metadata.DeliveryCulture ?? CultureInfo.CurrentUICulture;

                                    switch (Utility.ModelFileID(ds))
                                    {
                                        case Utility.ModelDBIDs.v7:
                                            this.logger?.LogInformation("Validating v7");
                                            var v7 = new Val70();
                                            logicOverride = (int v) =>
                                            {
                                                var logicPath = Path.GetFullPath(
                                                    Path.Combine(
                                                        AppDomain.CurrentDomain.BaseDirectory,
                                                        $".\\NASSCO\\NASSCOv7.logicx"));
                                                this.logger?.LogInformation($"Validating v7. Logic override at [{logicPath}]");
                                                try
                                                {
                                                    var ds = new DataSet();
                                                    ds.ReadData(logicPath);
                                                    return ds;
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.logger?.LogError(
                                                        ex,
                                                        $"Error while reading the logic file at [{logicPath}]");
                                                }

                                                return null;
                                            };
                                            v7.GetLogicOverride = logicOverride;

                                            if (culture.IsMetric() == false)
                                            {
                                                v7.UnitProblemReport = INASSCOValidation.UnitProblems.Imperial;
                                            }
                                            else
                                            {
                                                v7.UnitProblemReport = INASSCOValidation.UnitProblems.Metric;
                                            }

                                            validate = v7;
                                            break;
                                        case Utility.ModelDBIDs.v6:
                                            var v6 = new Val60();
                                            logicOverride = (int v) =>
                                            {
                                                var logicPath = Path.GetFullPath(
                                                    Path.Combine(
                                                        AppDomain.CurrentDomain.BaseDirectory,
                                                        $".\\NASSCO\\NASSCOv6.logicx"));
                                                this.logger?.LogInformation($"Validating v6. Logic override at [{logicPath}]");
                                                try
                                                {
                                                    var ds = new DataSet();
                                                    ds.ReadData(logicPath);
                                                    return ds;
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.logger?.LogError(
                                                        ex,
                                                        $"Error while reading the logic file at [{logicPath}]");
                                                }

                                                return null;
                                            };
                                            v6.GetLogicOverride = logicOverride;

                                            if (culture.IsMetric() == false)
                                            {
                                                v6.UnitProblemReport = INASSCOValidation.UnitProblems.Imperial;
                                            }
                                            else
                                            {
                                                v6.UnitProblemReport = INASSCOValidation.UnitProblems.Metric;
                                            }

                                            validate = v6;
                                            break;
                                        default:
                                            throw new DataException("Not NASSCO data");
                                    }

                                    var problems = new List<ValidationProblem>();
                                    validate.SkipUpdateToLatest = true;
                                    validate.SourceData = ds;
                                    validate.SourceFilePath = string.Empty;
                                    validate.Error += Error;
                                    validate.Problem += Problem;

                                    void Problem(
                                        DataRow sourceRow,
                                        string fieldName,
                                        string description,
                                        IValidation.DataProblem problemClass,
                                        InspectionTypes inspectionType,
                                        object id,
                                        string inspectionLabel,
                                        string subInspectionLabel)
                                    {
                                        problems.Add(new()
                                        {
                                            Description = description,
                                            FieldName = fieldName,
                                            Id = id,
                                            InspectionLabel = inspectionLabel,
                                            InspectionType = inspectionType,
                                            ProblemClass = problemClass,
                                            SubInspectionLabel = subInspectionLabel,
                                            SourceRow = sourceRow,
                                        });
                                    }

                                    void Error(object? sender, IEvents.ErrorEventArgs args)
                                    {
                                        this.logger?.LogError(args?.Exception, "Error in NASSCO validation/exchange.");
                                    }

#if SLOWDOWN
                                    await Task.Delay(10000, token);
#endif

                                    var success = false;
                                    var savedCulture = CultureInfo.CurrentUICulture;
                                    try
                                    {
                                        CultureInfo.CurrentUICulture = culture;
                                        success = validate.Validate();
                                    }
                                    catch (Exception ex)
                                    {
                                        this.logger?.LogError(ex, $"Error validating NASSCO data [{inspectionPath}] in project [{projectFilePath}].");
                                        throw;
                                    }
                                    finally
                                    {
                                        CultureInfo.CurrentUICulture = savedCulture;
                                        validate.Error -= Error;
                                        validate.Problem -= Problem;
                                        if (validate is Val60 version6)
                                        {
                                            version6.GetLogicOverride = null;
                                        }
                                        else if (validate is Val70 version7)
                                        {
                                            version7.GetLogicOverride = null;
                                        }
                                    }

                                    if (!success)
                                    {
                                        throw new InvalidDataException($"Inspection Data [{inspectionPath}] in project [{projectFilePath}] is not valid.");
                                    }

                                    if (problems.Any(p => p.ProblemClass.IsError()))
                                    {
                                        throw new InvalidDataException(
                                            $"Inspection Data [{inspectionPath}] in project [{projectFilePath}] is not valid.\r\n{string.Join("\r\n", problems.Select(p => $"{p.ProblemClass}:{p.Description}"))}");
                                    }

                                    // Exchange
                                    var exchange = default(INASSCOExchange);
                                    var exportPath = Path.Combine(
                                        inspectionPath!,
                                        pack.Metadata.Name.SanitizeFilename() + ".mdb");

                                    int.TryParse(
                                        string.Concat(
                                            inspectionObject!.InspectionTableName.Skip(
                                                TablePrefixes.Inspection.Length))
                                        .Trim('_'),
                                        out var i);

                                    var exchangeId = Utility.ExchangeDBIDs.Invalid;
                                    switch (Utility.ModelFileID(ds))
                                    {
                                        case Utility.ModelDBIDs.v7:
                                            this.logger?.LogInformation("Exchanging v7");
                                            switch (i)
                                            {
                                                case 1:
                                                    exchangeId = Utility.ExchangeDBIDs.MACPv7;
                                                    var emptyMACPv7ExchangePath = Path.GetFullPath(
                                                        Path.Combine(
                                                            AppDomain.CurrentDomain.BaseDirectory,
                                                            $".\\NASSCO\\MACPV706.src"));
                                                    if (project.IndividualNASSCOExchangeGenerate == true)
                                                    {
                                                        System.IO.File.Copy(emptyMACPv7ExchangePath, exportPath, true);
                                                    }

                                                    if (project.CombinedNASSCOExchangeGenerate == true &&
                                                    !System.IO.File.Exists(compositeExchangeDBPath))
                                                    {
                                                        System.IO.File.Copy(
                                                            emptyMACPv7ExchangePath,
                                                            compositeExchangeDBPath,
                                                            true);
                                                    }

                                                    exchange = new ExO70MHExch();
                                                    break;
                                                case 2:
                                                case 3:
                                                    exchangeId = Utility.ExchangeDBIDs.PLACPv7;
                                                    var emptyPLACPv7ExchangePath = Path.GetFullPath(
                                                        Path.Combine(
                                                            AppDomain.CurrentDomain.BaseDirectory,
                                                            $".\\NASSCO\\PACP_LACPv702.src"));
                                                    if (project.IndividualNASSCOExchangeGenerate == true)
                                                    {
                                                        System.IO.File.Copy(emptyPLACPv7ExchangePath, exportPath, true);
                                                    }

                                                    if (project.CombinedNASSCOExchangeGenerate == true &&
                                                    !System.IO.File.Exists(compositeExchangeDBPath))
                                                    {
                                                        System.IO.File.Copy(
                                                            emptyPLACPv7ExchangePath,
                                                            compositeExchangeDBPath,
                                                            true);
                                                    }

                                                    exchange = new ExO70PipeExch();
                                                    break;
                                                default:
                                                    throw new DataException("Not NASSCO data");
                                            }

                                            logicOverride = (int v) =>
                                            {
                                                var logicPath = Path.GetFullPath(
                                                    Path.Combine(
                                                        AppDomain.CurrentDomain.BaseDirectory,
                                                        $".\\NASSCO\\NASSCOv7.logicx"));
                                                this.logger?.LogInformation($"Validating v7. Logic override at [{logicPath}]");
                                                try
                                                {
                                                    var ds = new DataSet();
                                                    ds.ReadData(logicPath);
                                                    return ds;
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.logger?.LogError(
                                                        ex,
                                                        $"Error while reading the logic file at [{logicPath}]");
                                                }

                                                return null;
                                            };

                                            break;
                                        case Utility.ModelDBIDs.v6:
                                            this.logger?.LogInformation("Exchanging v6");
                                            switch (i)
                                            {
                                                case 1:
                                                    exchangeId = Utility.ExchangeDBIDs.MACPv6;
                                                    var emptyMACPv6ExchangePath = Path.GetFullPath(
                                                        Path.Combine(
                                                            AppDomain.CurrentDomain.BaseDirectory,
                                                            $".\\NASSCO\\MACPV605.src"));
                                                    if (project.IndividualNASSCOExchangeGenerate == true)
                                                    {
                                                        System.IO.File.Copy(emptyMACPv6ExchangePath, exportPath, true);
                                                    }

                                                    if (project.CombinedNASSCOExchangeGenerate == true &&
                                                    !System.IO.File.Exists(compositeExchangeDBPath))
                                                    {
                                                        System.IO.File.Copy(
                                                            emptyMACPv6ExchangePath,
                                                            compositeExchangeDBPath,
                                                            true);
                                                    }

                                                    exchange = new ExO60MHExch();
                                                    break;
                                                case 2:
                                                case 3:
                                                    exchangeId = Utility.ExchangeDBIDs.PLACPv6;
                                                    var emptyPLACPv6ExchangePath = Path.GetFullPath(
                                                        Path.Combine(
                                                            AppDomain.CurrentDomain.BaseDirectory,
                                                            $".\\NASSCO\\PACP_LACPv602.src"));
                                                    if (project.IndividualNASSCOExchangeGenerate == true)
                                                    {
                                                        System.IO.File.Copy(emptyPLACPv6ExchangePath, exportPath, true);
                                                    }

                                                    if (project.CombinedNASSCOExchangeGenerate == true &&
                                                    !System.IO.File.Exists(compositeExchangeDBPath))
                                                    {
                                                        System.IO.File.Copy(
                                                            emptyPLACPv6ExchangePath,
                                                            compositeExchangeDBPath,
                                                            true);
                                                    }

                                                    exchange = new ExO60PipeExch();
                                                    break;
                                                default:
                                                    throw new DataException("Not NASSCO data");
                                            }

                                            logicOverride = (int v) =>
                                            {
                                                var logicPath = Path.GetFullPath(
                                                    Path.Combine(
                                                        AppDomain.CurrentDomain.BaseDirectory,
                                                        $".\\NASSCO\\NASSCOv6.logicx"));
                                                this.logger?.LogInformation($"Validating v6. Logic override at [{logicPath}]");
                                                try
                                                {
                                                    var ds = new DataSet();
                                                    ds.ReadData(logicPath);
                                                    return ds;
                                                }
                                                catch (Exception ex)
                                                {
                                                    this.logger?.LogError(
                                                        ex,
                                                        $"Error while reading the logic file at [{logicPath}]");
                                                }

                                                return null;
                                            };

                                            break;
                                        default:
                                            throw new DataException("Not NASSCO data");
                                    }

                                    exchange.GetLogicOverride = logicOverride;
                                    exchange.CopySourceData = false;
                                    exchange.SourceData = ds;
                                    exchange.DestinationPath = exportPath;
                                    exchange.IsImperial = !culture.IsMetric();
                                    exchange.License = @"{""ExportTimeCode"":true}".Encrypt("NASSCO_ExV");
                                    exchange.MediaCompareMD5 = false;
                                    exchange.MediaFolderPath = string.Empty;
                                    exchange.MediaHandling = IExchange.MediaHandlings.NoCopy;
                                    exchange.MissingMediaContinue = true;
                                    exchange.PerformCustomNameMatching = true;
                                    exchange.SkipUpdateToLatest = true;
                                    exchange.SourcePath = inspectionObject.FileName;
                                    exchange.Error += Error;

                                    success = false;
                                    try
                                    {
                                        CultureInfo.CurrentUICulture = culture;
                                        if (project.IndividualNASSCOExchangeGenerate == true)
                                        {
                                            success = exchange.Exchange();
                                        }
                                        else
                                        {
                                            success = true;
                                        }

                                        if (success && project.CombinedNASSCOExchangeGenerate == true)
                                        {
                                            exchange.DestinationPath = compositeExchangeDBPath;
                                            success = exchange.Exchange();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        try
                                        {
                                            if (System.IO.File.Exists(exportPath))
                                            {
                                                System.IO.File.Delete(exportPath);
                                            }
                                        }
                                        catch (Exception)
                                        {
                                        }

                                        this.logger?.LogError(ex, $"Error exchanging data in inspection [{inspectionPath}] in project [{projectFilePath}].");
                                        throw;
                                    }
                                    finally
                                    {
                                        CultureInfo.CurrentUICulture = savedCulture;
                                        exchange.Error -= Error;
                                        exchange.GetLogicOverride = null;
                                    }

                                    if (!success)
                                    {
                                        throw new DataException($"Error exchanging data in inspection [{inspectionPath}] in project [{projectFilePath}].");
                                    }

                                    if (exchangeId == Utility.ExchangeDBIDs.PLACPv7 ||
                                    exchangeId == Utility.ExchangeDBIDs.MACPv7)
                                    {
                                        if (project.IndividualNASSCOExchangeGenerate == true)
                                        {
                                            Utility.GradeExchangeDB(exportPath, exchange.IsImperial);
                                        }

                                        if (project.CombinedNASSCOExchangeGenerate == true)
                                        {
                                            Utility.GradeExchangeDB(compositeExchangeDBPath, exchange.IsImperial);
                                        }
                                    }

                                    if (project.IndividualNASSCOExchangeGenerate == true)
                                    {
                                        await dq.EnqueueAsync(() =>
                                        {
                                            inspection.ExchangeDBCompletePath = exportPath;
                                        });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                this.logger?.LogError(ex, "Error individual NASSCO Exchange");
                                throw;
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspection.State = DownloadInspection.States.Complete;
                            });

                            await this.WriteProject(project);
                        }
                        catch (Exception ex)
                        {
                            if (inspection is not null)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    inspection.State = DownloadInspection.States.Errored;
                                    inspection.LastError = $"Error during inspection process\r\n\r\nError details:\r\n{ex}";
                                });
                            }

                            inspectionErrors.Add(ex);
                            return;
                        }
                        finally
                        {
                            if (inspectionObject is not null)
                            {
                                await dq.EnqueueAsync(() =>
                                {
                                    try
                                    {
                                        inspectionObject?.Dispose();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                });
                            }
                        }
                    });
            }

            await dq.EnqueueAsync(() =>
            {
                project?.RaiseProgressChanged();
            });

            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            if (inspectionErrors.Any())
            {
                throw new AggregateException(inspectionErrors.ToArray());
            }

            if (project.Inspections?.Any(h => h.Inspection?.State == DownloadInspection.States.Paused) == true)
            {
                throw new TaskCanceledException();
            }

            // Do the combined stuff
            var document = default(IDocument);
            try
            {
                if (project.CombinedReportIds?.Any() == true)
                {
                    document = this.documentFactory.GetDocument();
                    document.Author = "Industrial Technology Group";
                    document.Culture = CultureInfo.CurrentUICulture;

                    // First look for table of contents report first
                    foreach (var report in project.CombinedReportIds
                        .Select(rId => this.reportRegistry.GetReport<ITableOfContentsReportDefinition>(rId))
                        .Where(r => r is not null))
                    {
                        if (report is ILetterheadAware letterheadAware && inspectionType is not null)
                        {
                            letterheadAware.Letterhead = firstPack?.GetReportLetterheadDefinition(inspectionType);
                        }

                        if (report is ICanRenderSalesWatermark sales)
                        {
                            sales.ShouldRenderSalesWatermark = false;
                        }

                        if (report is ICanRenderSupportAndTestingWatermark support)
                        {
                            support.ShouldRenderSupportAndTestingWatermark = false;
                        }

                        document.AddReport(report);
                    }

                    var inspectionPathsCount = project.Inspections?.Count() ?? 0;

                    async Task<InspectionBase?> GetInspection(DownloadInspection? inspection)
                    {
                        if (inspection is null)
                        {
                            return null;
                        }

                        var inspectionObject = default(Inspection);
                        try
                        {
                            await dq.EnqueueAsync(async () =>
                            {
                                inspectionObject = (await this.inspectionFactory.OpenInspectionAsync(
                                inspection.DataCompletePath,
                                Guid.Empty)) as Inspection;
                            });
                        }
                        catch (Exception ex)
                        {
                            this.logger?.LogWarning(
                                ex,
                                $"Error loading inspection for composite reports [{inspection.DataCompletePath}]");
                        }

                        if (inspectionObject is null)
                        {
                            var ds = this.jsonDeserializer.Deserialize(inspection.Json?.ToString() ?? string.Empty);

                            if (!ds.TryGetInspectionRow(out var inspectionRow) || inspectionRow is null)
                            {
                                return null;
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspectionObject = this.inspectionFactory.InitializeInspection(
                                firstPack,
                                ds,
                                inspectionRow.Table.TableName) as Inspection;
                            });

                            if (inspectionObject is null)
                            {
                                return null;
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspectionObject.AssetCollection.View.MoveCurrentToFirst();
                                inspectionObject.InspectionCollection.View.MoveCurrentToFirst();
                                inspectionObject.FileName = Path.Combine(
                                    inspection.DownloadPath ?? ".\\",
                                    inspectionObject.ResolveInspectionFileNameWithoutExtension() + ".ptdx");

                                var currentOk = inspectionObject.InspectionObservationFocusCollection.View.MoveCurrentToFirst();
                                while (currentOk)
                                {
                                    var of = inspectionObject.CurrentInspectionObservationFocusRowView;
                                    var mp = inspectionObject.GetFieldRawValue(of, of.Row.Table.TableName, "Media_Parameters");
                                    if (mp is string mpString && mpString.EndsWith("t") &&
                                    double.TryParse(mpString.TrimEnd('t'), out var time))
                                    {
                                        var ts = TimeSpan.FromMilliseconds(time);
                                        of.SetColumnValue(
                                            "Media_Parameters",
                                            ts.TotalMilliseconds.ToString().Encrypt(inspectionObject.GetInspectionGuid().ToString()));
                                    }

                                    currentOk = inspectionObject.InspectionObservationFocusCollection.View.MoveCurrentToNext();
                                }

                                // Change the media paths to local-relative
                                foreach (var mediaReference in inspectionObject.MediaReferences.ToArray())
                                {
                                    inspectionObject.MediaReferences.Replace(
                                        mediaReference,
                                        mediaReference.UpdatePath(
                                            new Uri(
                                                Uri.UnescapeDataString(Path.GetFileName(mediaReference.URI.OriginalString)),
                                                UriKind.Relative)));
                                }
                            });
                        }

                        return inspectionObject;
                    }

                    foreach (var report in project.CombinedReportIds
                        .Select(rId => this.reportRegistry.GetReport<IMultiInspectionReportDefinition>(rId))
                        .Where(r => r is not null))
                    {
                        var count = 0;
                        report.NextInspectionAsync = async (reset) =>
                        {
                            try
                            {
                                if (reset)
                                {
                                    count = 0;
                                }

                                if (count >= inspectionPathsCount)
                                {
                                    return (null, count, inspectionPathsCount, true);
                                }

                                var inspection = await GetInspection(project.Inspections!.ElementAt(count)?.Inspection);
                                return (inspection, count, inspectionPathsCount, false);
                            }
                            catch
                            {
                                return (null, count, inspectionPathsCount, false);
                            }
                            finally
                            {
                                count++;
                            }
                        };

                        if (report is ILetterheadAware letterheadAware && inspectionType is not null)
                        {
                            letterheadAware.Letterhead = firstPack?.GetReportLetterheadDefinition(inspectionType);
                        }

                        if (report is ICanRenderSalesWatermark sales)
                        {
                            sales.ShouldRenderSalesWatermark = false;
                        }

                        if (report is ICanRenderSupportAndTestingWatermark support)
                        {
                            support.ShouldRenderSupportAndTestingWatermark = false;
                        }

                        document.AddReport(report);
                    }

                    if (project.Inspections is not null)
                    {
                        foreach (var inspectionHandler in project.Inspections)
                        {
                            foreach (var report in project.CombinedReportIds
                                .Select(rId => this.reportRegistry.GetReport<IInspectionReportDefinition>(rId))
                                .Where(r => r is not null))
                            {
                                report.DisposeInspectionWhenComplete = true;
                                report.GetInspectionAsync = async () =>
                                {
                                    try
                                    {
                                        return await GetInspection(inspectionHandler.Inspection);
                                    }
                                    catch (Exception)
                                    {
                                        return null;
                                    }
                                };

                                if (report is ILetterheadAware letterheadAware && inspectionType is not null)
                                {
                                    letterheadAware.Letterhead = firstPack?.GetReportLetterheadDefinition(inspectionType);
                                }

                                if (report is ICanRenderSalesWatermark sales)
                                {
                                    sales.ShouldRenderSalesWatermark = false;
                                }

                                if (report is ICanRenderSupportAndTestingWatermark support)
                                {
                                    support.ShouldRenderSupportAndTestingWatermark = false;
                                }

                                if (report is ICanRenderEnumeratedWatermark watermark)
                                {
                                    watermark.Watermark = ICanRenderEnumeratedWatermark.Watermarks.None;
                                }

                                document.AddReport(report);
                            }
                        }
                    }

#if SLOWDOWN
                    await Task.Delay(10000, token);
#endif

                    await dq.EnqueueAsync(async () =>
                    {
                        using (var fs = new FileStream(compositeReportPath, FileMode.Create))
                        {
                            await document.GeneratePackage(fs);
                        }
                    });

                    if (document is IDisposable dis)
                    {
                        dis.Dispose();
                    }
                }
            }
            finally
            {
                if (document is IDisposable d)
                {
                    d.Dispose();
                }

                GC.Collect();
            }

            if (project.Inspections is not null)
            {
                foreach (var i in project.Inspections.Select(i => i.Inspection).Where(i => i is not null))
                {
                    switch (i!.State)
                    {
                        case DownloadInspection.States.Queued:
                        case DownloadInspection.States.Processing:
                        case DownloadInspection.States.Staged:
                            await dq.EnqueueAsync(() =>
                            {
                                i.State = DownloadInspection.States.Complete;
                            });
                            break;
                    }
                }
            }

            if (project is not null)
            {
                await dq.EnqueueAsync(() =>
                {
                    project.WasCompleted = true;
                    project.RaiseProgressChanged();
                    project.RaiseStateChanged();
                });

                await this.WriteProject(project);
            }
        }
        catch (TaskCanceledException)
        {
            this.logger?.LogInformation($"Downloading project [{projectFilePath}] was cancelled");
            if (project != default)
            {
                try
                {
                    if (!string.IsNullOrEmpty(compositeReportPath) &&
                        System.IO.File.Exists(compositeReportPath))
                    {
                        System.IO.File.Delete(compositeReportPath);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (!string.IsNullOrEmpty(compositeExchangeDBPath) &&
                        System.IO.File.Exists(compositeExchangeDBPath))
                    {
                        System.IO.File.Delete(compositeExchangeDBPath);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (project.Inspections is not null)
                    {
                        foreach (var inspection in project.Inspections)
                        {
                            if (inspection.Inspection is null ||
                                inspection.Inspection.State != DownloadInspection.States.Complete)
                            {
                                continue;
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspection.Inspection.State = DownloadInspection.States.Paused;
                                inspection.Inspection.LastError = "Inspection paused";
                            });
                        }

                        await this.WriteProject(project);
                    }
                }
                catch (Exception ex2)
                {
                    this.logger?.LogError(ex2, $"Error in error handler while downloading project [{projectFilePath}]");
                }
            }
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Error download project [{projectFilePath}]");
            if (project != default)
            {
                try
                {
                    if (!string.IsNullOrEmpty(compositeReportPath) &&
                        System.IO.File.Exists(compositeReportPath))
                    {
                        System.IO.File.Delete(compositeReportPath);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (!string.IsNullOrEmpty(compositeExchangeDBPath) &&
                        System.IO.File.Exists(compositeExchangeDBPath))
                    {
                        System.IO.File.Delete(compositeExchangeDBPath);
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (project.Inspections is not null)
                    {
                        foreach (var inspection in project.Inspections)
                        {
                            if (inspection.Inspection is null ||
                                inspection.Inspection.State != DownloadInspection.States.Complete)
                            {
                                continue;
                            }

                            await dq.EnqueueAsync(() =>
                            {
                                inspection.Inspection.State = Models.DownloadInspection.States.Errored;
                            });
                        }

                        await this.WriteProject(project);
                    }
                }
                catch (Exception ex2)
                {
                    this.logger?.LogError(ex2, $"Error in error handler while downloading project [{projectFilePath}]");
                }
            }
            else
            {
                throw;
            }
        }
    }

    private async Task<(long? Size, bool AcceptsRangeAsBytes, string? Etag)> GetUriInfo(Uri uri)
    {
        using var httpClient = this.serviceProvider.GetService(typeof(HttpClient)) as HttpClient;

        if (httpClient is null)
        {
            throw new NullReferenceException("Unable to create http client.");
        }

        return await HttpHelper.GetUriInfo(uri, httpClient);
    }

    private void Source_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var oldItem in e.OldItems)
            {
                if (oldItem is not Project p || string.IsNullOrEmpty(p.FilePath))
                {
                    continue;
                }

                foreach (var j in this.FindJobForProject(p))
                {
                    this.jobClient.Delete(j.Key);
                }
            }
        }

        if (e.NewItems is not null)
        {
            var localItems = e.NewItems.Cast<Project>().ToArray();
            Task.Run(async () =>
            {
                foreach (var newItem in localItems)
                {
                    if (newItem is not Project project || string.IsNullOrEmpty(project.FilePath))
                    {
                        continue;
                    }

                    if (project.Inspections?.Count > 0)
                    {
                        if (this.FindJobForProject(project).FirstOrDefault().Value is not Job job && project.WasCompleted != true)
                        {
                            var createJob = false;
                            var userCreateJob = false;

                            // There is no pre-existing job, so check if we need one.
                            //
                            // If any of the inspections in the project are not complete
                            // or if any of the inspections in the project don't have a .ptdx that exists?
                            if (!createJob && project.Inspections is not null &&
                            project.Inspections
                            .Any(i => i.Inspection?.State != DownloadInspection.States.Complete ||
                                string.IsNullOrEmpty(i.Inspection?.DataCompletePath) ||
                                !System.IO.File.Exists(i.Inspection.DataCompletePath)))
                            {
                                // If all the inspections are errored or paused, we are not going to
                                // automatically create this job. We'll let the user do it by hand.
                                if (project.Inspections
                                .Any(i => i.Inspection?.State != DownloadInspection.States.Errored &&
                                i.Inspection?.State != DownloadInspection.States.Paused))
                                {
                                    createJob = true;
                                }
                                else
                                {
                                    userCreateJob = true;
                                }
                            }

                            // If the combined nassco exchange is required, is it there?
                            if (!createJob && !userCreateJob &&
                            project.CombinedNASSCOExchangeGenerate == true &&
                            project.GetExchangeDBPath() is string dbPath &&
                            !string.IsNullOrEmpty(dbPath) &&
                            !System.IO.File.Exists(dbPath))
                            {
                                // Create the job
                                createJob = true;
                            }

                            // If the combined reports are required, is it there?
                            if (!createJob && !userCreateJob &&
                            project.CombinedReportIds?.Any() == true &&
                            project.GetCombinedReportPath() is string reportPath &&
                            !string.IsNullOrEmpty(reportPath) &&
                            !System.IO.File.Exists(reportPath))
                            {
                                // Create the job
                                createJob = true;
                            }

                            // If any of the inspections require a report, is it there?
                            if (!createJob && !userCreateJob &&
                            project.Inspections is not null &&
                            project.IndividualReportIds?.Any() == true &&
                            project.Inspections
                            .Any(i => string.IsNullOrEmpty(i.Inspection?.ReportCompletePath) ||
                                !System.IO.File.Exists(i.Inspection.ReportCompletePath)))
                            {
                                createJob = true;
                            }

                            // If any of the inspection require a NASSCO exchange database, is it there?
                            if (!createJob && !userCreateJob &&
                            project.Inspections is not null &&
                            project.IndividualNASSCOExchangeGenerate == true &&
                            project.Inspections
                            .Any(i => string.IsNullOrEmpty(i.Inspection?.ExchangeDBCompletePath) ||
                                !System.IO.File.Exists(i.Inspection.ExchangeDBCompletePath)))
                            {
                                createJob = true;
                            }

                            if (createJob && !userCreateJob)
                            {
                                await this.CreateJobForProject(project);
                            }
                        }
                    }
                }
            });
        }
    }

    private async Task WriteProject(Project project)
    {
        var projectJson = project.ToJson();
        var release = false;
        try
        {
            await ProjectWritingSemaphore.WaitAsync();
            release = true;
            System.IO.File.WriteAllText(project.FilePath!, projectJson);
        }
        catch (Exception)
        {
        }
        finally
        {
            if (release)
            {
                ProjectWritingSemaphore.Release();
            }
        }
    }

#if DEBUG
    /// <inheritdoc/>
#pragma warning disable SA1202 // Elements should be ordered by access
    public async Task Test(CancellationToken token)
#pragma warning restore SA1202 // Elements should be ordered by access
    {
        for (var i = 0; i < 10; i++)
        {
            await Task.Delay(1000, token);
            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }
        }
    }
#endif
}
