// <copyright file="DownloadInspectionHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PT.Inspection;
using PT.Inspection.Inspections;
using PT.Inspection.Model;
using PT.Inspection.Packs;
using PT.Inspection.Templates;
using PT.Inspection.Wpf;
using Refit;
using static PipeTech.Downloader.Models.DownloadInspection;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Download inspection handler class.
/// </summary>
public partial class DownloadInspectionHandler : BindableRecipient, IDisposable
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<DownloadInspectionHandler>? logger;

    private DownloadInspection? dlInspection;

    private CancellationTokenSource? tokenSource;

    /// <summary>
    /// Gets or sets a value indicating whether the item is expanded.
    /// </summary>
    [ObservableProperty]
    [JsonIgnore]
    private bool expanded = false;

    /// <summary>
    /// Gets or sets the pack Id.
    /// </summary>
    [ObservableProperty]
    private Guid? packId;

    /// <summary>
    /// Gets or sets the last error.
    /// </summary>
    [ObservableProperty]
    private string? lastError;

    /// <summary>
    /// Gets or sets a value indicating whether to skip.
    /// </summary>
    [ObservableProperty]
    private bool skip;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadInspectionHandler"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    /// <param name="logger">Logger service.</param>
    public DownloadInspectionHandler(
        IServiceProvider serviceProvider,
        ILogger<DownloadInspectionHandler>? logger = null)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;

        this.ReloadCommand = new(this.ExecuteReload);
    }

    /// <summary>
    /// Gets the reload command.
    /// </summary>
    public RelayCommand ReloadCommand
    {
        get;
    }

    /// <summary>
    /// Gets or sets the inspection this handles.
    /// </summary>
    public DownloadInspection? Inspection
    {
        get => this.dlInspection; set => this.SetProperty(ref this.dlInspection, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the skip the load process.
    /// </summary>
    public bool BypassLoad { get; set; } = false;

    /// <inheritdoc/>
    public void Dispose()
    {
        this.tokenSource?.Cancel();
        while (this.Inspection?.Files.Count > 0)
        {
            this.Inspection?.Files.RemoveAt(0);
        }

        this.Inspection?.Dispose();
        this.Inspection = null;
    }

    /// <summary>
    /// Load inspection.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Asynchronous Task.</returns>
    /// <exception cref="NullReferenceException">Json is empty.</exception>
    internal async Task LoadInspection(CancellationToken token = default)
    {
        ////await Task.Yield();

        this.LastError = string.Empty;

        if (this.Inspection is null || this.BypassLoad)
        {
            return;
        }

        await Task.Run(async () =>
        {
            var inspection = default(Inspection);
            try
            {
                await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                {
                    this.Inspection.State = States.Loading;
                });

#if SLOWDOWN
            await Task.Delay(10000, token);
#endif

                if (this.Inspection.Json is null ||
                    string.IsNullOrEmpty(this.Inspection.Json?.ToString()))
                {
                    var message = $"{nameof(this.Inspection.Json)} is empty.";
                    this.LastError = message;
                    throw new NullReferenceException(message);
                }

                var ele = this.Inspection.Json;
                if (ele is null || !ele.Value.TryGetProperty("$packId", out var value) ||
                    !Guid.TryParse(value.ToString(), out var packId))
                {
                    var message = $"Unable to retrieve pack Id of inspection.";
                    this.LastError = message;
                    throw new Exception(message);
                }

                if (this.serviceProvider.GetService(typeof(ITemplateRegistry)) is not ITemplateRegistry tm)
                {
                    var message = "Unable to retrieve the template registry.";
                    this.LastError = message;
                    throw new NullReferenceException(message);
                }

                var pack = tm.InstalledTemplates?
                    .Where(p => p.Metadata.ID == packId)
                    .OrderByDescending(p => p.Metadata.Version)
                    .FirstOrDefault();

                if (pack is null)
                {
                    // Get the pack from external services.
                    using var httpClient = this.serviceProvider.GetService(typeof(HttpClient)) as HttpClient;
                    if (httpClient is not null)
                    {
                        httpClient.BaseAddress = new(@"http://100.24.161.59:5000");
                    }

                    var externalServices = RestService.For<IExternalServices>(
                         httpClient!,
                         new RefitSettings(
                             new SystemTextJsonContentSerializer(new()
                             {
                                 AllowTrailingCommas = true,
                                 PropertyNameCaseInsensitive = true,
                                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                             })));

                    var tempFile = PathWrapper.GetTempFileNameUnique();
                    var tempFileInfo = default(FileInfo);
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            throw new TaskCanceledException();
                        }

#if SLOWDOWN
                    await Task.Delay(10000, token);
#endif

                        var info = await externalServices.GetPack(packId, descending: true, token: token);
                        if (info.Content is null)
                        {
                            var message = $"Unable to download pack [{packId}]. Code: [{info.StatusCode}]";
                            if (info.Error is not null)
                            {
                                message += $"\r\n{info.Error}";
                            }

                            this.LastError = message;
                            throw new Exception(message);
                        }

                        if (token.IsCancellationRequested)
                        {
                            throw new TaskCanceledException();
                        }

                        using (var fs = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite))
                        {
                            info.Content.CopyTo(fs);
                        }

                        if (this.serviceProvider?.GetService(typeof(IPackFactory)) is not IPackFactory pf)
                        {
                            throw new NullReferenceException("Unable to retrieve pack factory service.");
                        }

                        var tempPack = pf.OpenPackFile(tempFile);
                        if (pf is IDisposable d)
                        {
                            d.Dispose();
                        }

                        tempFileInfo = new FileInfo(tempFile);
                        var dirTemp = Path.Combine(
                            tempFileInfo.DirectoryName!,
                            Path.GetFileNameWithoutExtension(tempFileInfo.Name));
                        if (!Directory.Exists(dirTemp))
                        {
                            Directory.CreateDirectory(dirTemp);
                        }

                        tempFileInfo.MoveTo(
                            Path.Combine(
                                dirTemp,
                                $"{tempPack.Metadata.Name}_v{tempPack.Metadata.Version}.pttemplate"),
                            true);

                        await tm.ImportTemplateAsync(tempFileInfo.FullName, TemplateRegistryKnownLocationType.Machine);

                        pack = tm.InstalledTemplates?
                            .Where(p => p.Metadata.ID == packId)
                            .OrderByDescending(p => p.Metadata.Version)
                            .FirstOrDefault();
                    }
                    finally
                    {
                        if (externalServices is IDisposable d)
                        {
                            d.Dispose();
                        }

                        if (System.IO.File.Exists(tempFile))
                        {
                            System.IO.File.Delete(tempFile);
                        }

                        if (tempFileInfo?.ExistsFeatRefresh() == true)
                        {
                            tempFileInfo.Delete();
                        }

                        if (tempFileInfo?.Directory is DirectoryInfo di &&
                        di.Exists)
                        {
                            di.Delete(true);
                        }
                    }
                }

                this.PackId = pack?.Metadata?.ID;

                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                var deserilizer = this.serviceProvider.GetService(typeof(IJsonDeserializerV2)) as IJsonDeserializerV2;
                using var ds = deserilizer?.Deserialize(pack, this.Inspection.Json?.ToString(), false);

                if (ds is null)
                {
                    var message = $"Unable to deserialize inspection.";
                    this.LastError = message;
                    throw new Exception(message);
                }

                if (!ds.TryGetInspectionRow(out var drInspection) || drInspection is null)
                {
                    var message = $"Unable to deserialize an inspection.";
                    this.LastError = message;
                    throw new Exception(message);
                }

                var inspectionFactory = this.serviceProvider.GetService(typeof(IInspectionFactory)) as IInspectionFactory;
                await App.MainWindow.DispatcherQueue.EnqueueAsync(
                    () =>
                    {
                        inspection = inspectionFactory?.InitializeInspection(pack, ds, drInspection.Table.TableName) as Inspection;
                        inspection?.AssetCollection.View.MoveCurrentToFirst();
                        inspection?.InspectionCollection.View.MoveCurrentToFirst();
                    },
                    DispatcherQueuePriority.Low);

                var grouping = inspection?.ResolveInspectionFolderGroup();
                if (grouping?.Count > 0)
                {
                    var name = string.Join("/", grouping.Select(n => n.SanitizeFilename()));

                    await App.MainWindow.DispatcherQueue.EnqueueAsync(
                        () =>
                        {
                            this.Inspection.Name = name;
                        },
                        DispatcherQueuePriority.Low);
                }
                else
                {
                    await App.MainWindow.DispatcherQueue.EnqueueAsync(
                        () =>
                        {
                            var name = inspection?.ResolveInspectionFolderName().SanitizeFilename();
                            this.Inspection.Name = name;
                        },
                        DispatcherQueuePriority.Low);
                }

                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException();
                }

                var filename = default(string?);
                await App.MainWindow.DispatcherQueue.EnqueueAsync(
                    () =>
                    {
                        filename = inspection.ResolveInspectionFileNameWithoutExtension();
                        this.Inspection.Files.Add(new() { Name = filename + ".ptdx" });
                    },
                    DispatcherQueuePriority.Low);

                if (inspection?.MediaReferences is not null)
                {
                    var httpClient = this.serviceProvider.GetService(typeof(HttpClient)) as HttpClient;

                    foreach (var media in inspection.MediaReferences)
                    {
                        if (token.IsCancellationRequested)
                        {
                            throw new TaskCanceledException();
                        }

                        if (media.ExistsLocal())
                        {
                            var f = new File()
                            {
                                Name = media.GetAbsoluteUri().AbsoluteUri,
                                Size = new FileInfo(media.GetAbsoluteUri().LocalPath).Length,
                            };

                            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                                () =>
                                {
                                    this.Inspection.Files.Add(f);
                                },
                                DispatcherQueuePriority.Low);
                        }
                        else
                        {
                            var f = new File()
                            {
                                Name = media.URI.OriginalString,
                                Size = media.Length,
                            };

                            if ((f.Size is null || f.Size == 0) && httpClient is not null)
                            {
                                try
                                {
                                    var info = await HttpHelper.GetUriInfo(media.URI, httpClient);
                                    if (info.Size is not null)
                                    {
                                        f.Size = info.Size;
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                                () =>
                                {
                                    this.Inspection.Files.Add(f);
                                },
                                DispatcherQueuePriority.Low);
                        }
                    }
                }

                await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                {
                    this.Inspection.State = States.Staged;
                });
            }
            catch (TaskCanceledException)
            {
                var message = $"Loading download inspection paused.";
                this.LastError = message;
                this.logger?.LogWarning(message);
                if (this.Inspection is not null)
                {
                    await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                    {
                        this.Inspection.State = States.Paused;
                        this.Inspection.LastError = $"{message}";
                    });
                }
            }
            catch (Exception ex)
            {
                var message = "Error loading download inspection.";
                if (string.IsNullOrEmpty(this.LastError))
                {
                    this.LastError = $"{message}: {ex}";
                }

                this.logger?.LogError(ex, message);
                if (this.Inspection is not null)
                {
                    await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                    {
                        this.Inspection.State = States.Errored;
                        this.Inspection.LastError = $"{message}\r\n\r\nError details:\r\n{ex}";
                    });
                }
            }
            finally
            {
                if (inspection is not null)
                {
                    await App.MainWindow.DispatcherQueue.EnqueueAsync(
                        () =>
                        {
                            inspection.Dispose();
                        },
                        DispatcherQueuePriority.Low);
                }

                ////await Task.Yield();
            }
        });
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);
        switch (e.PropertyName)
        {
            case nameof(this.Inspection):
                if (this.Inspection is not null)
                {
                    this.Inspection.PropertyChanged -= this.DownloadInspection_PropertyChanged;
                }

                break;
            default:
                break;
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        switch (e.PropertyName)
        {
            case nameof(this.Inspection):
                if (this.Inspection is not null)
                {
                    this.Inspection.PropertyChanged += this.DownloadInspection_PropertyChanged;
                    if (!this.BypassLoad)
                    {
                        this.tokenSource?.Cancel();
                        this.tokenSource = new();
                        _ = this.LoadInspection(this.tokenSource.Token);
                    }
                }

                break;
            default:
                break;
        }
    }

    private void DownloadInspection_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspection.Json):
                if (!this.BypassLoad)
                {
                    this.tokenSource?.Cancel();
                    this.tokenSource = new();
                    _ = this.LoadInspection(this.tokenSource.Token);
                }

                break;
            default:
                break;
        }
    }

    private void ExecuteReload()
    {
        this.tokenSource?.Cancel();
        this.tokenSource = new();

        while (this.Inspection?.Files?.Count > 0)
        {
            this.Inspection?.Files?.RemoveAt(0);
        }

        _ = this.LoadInspection(this.tokenSource.Token);
    }
}
