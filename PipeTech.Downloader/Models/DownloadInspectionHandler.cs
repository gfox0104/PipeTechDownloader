// <copyright file="DownloadInspectionHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text.Json;
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
    private bool expanded = false;

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
        await Task.Yield();

        if (this.Inspection is null || this.BypassLoad)
        {
            return;
        }

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
                throw new NullReferenceException($"{nameof(this.Inspection.Json)} is empty.");
            }

            var ele = this.Inspection.Json;
            if (ele is null || !ele.Value.TryGetProperty("$packId", out var value) ||
                !Guid.TryParse(value.ToString(), out var packId))
            {
                throw new Exception($"Unable to retrieve pack Id of inspection.");
            }

            var tm = this.serviceProvider.GetService(typeof(ITemplateRegistry)) as ITemplateRegistry;
            var pack = tm?.InstalledTemplates?
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
                        throw new Exception("Unable to download pack");
                    }

                    if (token.IsCancellationRequested)
                    {
                        throw new TaskCanceledException();
                    }

                    using (var fs = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite))
                    {
                        info.Content.CopyTo(fs);
                    }

                    var pf = this.serviceProvider.GetService(typeof(IPackFactory)) as IPackFactory;
                    pack = pf?.OpenPackFile(tempFile);
                    tm?.Register(pack, TemplateRegistryKnownLocationType.Machine);
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
                }
            }

            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            var deserilizer = this.serviceProvider.GetService(typeof(IJsonDeserializerV2)) as IJsonDeserializerV2;
            using var ds = deserilizer?.Deserialize(pack, this.Inspection.Json?.ToString(), false);

            if (ds is null)
            {
                throw new Exception($"Unable to deserialize inspection.");
            }

            if (!ds.TryGetInspectionRow(out var drInspection) || drInspection is null)
            {
                throw new Exception($"Unable to deserialize an inspection.");
            }

            var inspectionFactory = this.serviceProvider.GetService(typeof(IInspectionFactory)) as IInspectionFactory;
            using var inspection = inspectionFactory?.InitializeInspection(pack, ds, drInspection.Table.TableName) as Inspection;

            inspection?.AssetCollection.View.MoveCurrentToFirst();
            inspection?.InspectionCollection.View.MoveCurrentToFirst();

            var grouping = inspection?.ResolveInspectionFolderGroup();
            if (grouping?.Count > 0)
            {
                var name = string.Join("/", grouping.Select(n => n.SanitizeFilename()));
                this.Inspection.Name = name;
            }
            else
            {
                var name = inspection?.ResolveInspectionFolderName().SanitizeFilename();
                this.Inspection.Name = name;
            }

            if (token.IsCancellationRequested)
            {
                throw new TaskCanceledException();
            }

            var filename = inspection.ResolveInspectionFileNameWithoutExtension();

            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                () =>
                {
                    this.Inspection.Files.Add(new() { Name = filename + ".ptdx" });
                },
                DispatcherQueuePriority.Normal);

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
                            DispatcherQueuePriority.Normal);
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
                            DispatcherQueuePriority.Normal);
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
            this.logger?.LogWarning($"Loading download inspection paused.");
            if (this.Inspection is not null)
            {
                await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                {
                    this.Inspection.State = States.Paused;
                });
            }
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error loading download inspection.");
            if (this.Inspection is not null)
            {
                await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                {
                    this.Inspection.State = States.Errored;
                });
            }
        }
        finally
        {
            await Task.Yield();
        }
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
