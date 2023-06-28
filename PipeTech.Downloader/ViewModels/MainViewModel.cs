// <copyright file="MainViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Core.Contracts;
using PipeTech.Downloader.Core.Models;
using PipeTech.Downloader.Models;
using PT.Inspection;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Main view model class.
/// </summary>
public partial class MainViewModel : BindableRecipient, INavigationAware, IDisposable
{
    private static readonly string LASTDATAFOLDERSETTING = "LastDataFolder";

    private readonly INavigationService navigationService;
    private readonly IHubService hubService;
    private readonly ILogger<MainViewModel>? logger;
    private readonly ILocalSettingsService localSettingsService;
    private readonly IServiceProvider serviceProvider;
    private readonly SettingsDirectoryPaths paths;

    [ObservableProperty]
    private string? downloadName;

    [ObservableProperty]
    private int? totalCount;

    [ObservableProperty]
    private bool manifestLoading = true;

    [ObservableProperty]
    private string? dataFolder;

    [ObservableProperty]
    private bool useDefault = false;

    private Uri? manifestUri;

    private CancellationTokenSource? tokenSource;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="hubService">Hub service.</param>
    /// <param name="serviceProvider">Service provider.</param>
    /// <param name="localSettingsService">Local settings service.</param>
    /// <param name="options">Options for settings directory paths.</param>
    /// <param name="logger">Logger service.</param>
    public MainViewModel(
        INavigationService navigationService,
        IHubService hubService,
        IServiceProvider serviceProvider,
        ILocalSettingsService localSettingsService,
        IOptions<SettingsDirectoryPaths> options,
        ILogger<MainViewModel>? logger = null)
    {
        this.navigationService = navigationService;
        this.hubService = hubService;
        this.serviceProvider = serviceProvider;
        this.localSettingsService = localSettingsService;
        this.paths = options.Value;
        this.logger = logger;

        this.Inspections = new ObservableCollection<object>();
        this.Inspections.CollectionChanged += this.Inspections_CollectionChanged;
        this.CloseCommand = new RelayCommand(() =>
        {
            this.navigationService.GoBack();
        });

        this.DownloadCommand = new AsyncRelayCommand(this.ExecuteDownload, this.CanExecuteDownload);
        this.BrowseFolderCommand = new RelayCommand(this.ExecuteBrowseDataFolder);

        _ = Task.Run(async () =>
        {
            this.DataFolder = await this.localSettingsService.ReadSettingAsync<string?>(LASTDATAFOLDERSETTING);
        });
    }

    /// <summary>
    /// Gets the download command.
    /// </summary>
    public ICommand DownloadCommand
    {
        get;
    }

    /// <summary>
    /// Gets the browse folder command.
    /// </summary>
    public ICommand BrowseFolderCommand
    {
        get;
    }

    /// <summary>
    /// Gets the close command.
    /// </summary>
    public ICommand CloseCommand
    {
        get;
    }

    /// <summary>
    /// Gets the inspections/combined objects.
    /// </summary>
    public ObservableCollection<object> Inspections
    {
        get;
    }

    /// <summary>
    /// Gets or sets the manifest.
    /// </summary>
    public Manifest? Manifest
    {
        get; protected set;
    }

    /// <summary>
    /// Gets the inspection count string.
    /// </summary>
    public string? InspectionCountString
    {
        get
        {
            var value = $"{this.Inspections.Where(item => item is DownloadInspectionHandler).Count()}";
            if (this.TotalCount is int totalCount)
            {
                return value + $" of {totalCount}";
            }

            return value;
        }
    }

    /// <summary>
    /// Gets the total size.
    /// </summary>
    public long? TotalSize
    {
        get
        {
            if (this.Inspections?.Any() == true)
            {
                var sizeInBytes = 0L;
                try
                {
                    foreach (var i in this.Inspections
                        .Select(item => item as DownloadInspectionHandler)
                        .Where(item => item is not null))
                    {
                        sizeInBytes += i!.Inspection?.TotalSize ?? 0;
                    }
                }
                catch (Exception)
                {
                }

                return sizeInBytes;
            }

            return null;
        }
    }

    /////// <summary>
    /////// Gets the total size string in MB.
    /////// </summary>
    ////public string? TotalSizeinMB
    ////{
    ////    get
    ////    {
    ////        var sizeInBytes = 0L;
    ////        try
    ////        {
    ////            foreach (var i in this.Inspections
    ////                .Select(item => item as DownloadInspectionHandler)
    ////                .Where(item => item is not null))
    ////            {
    ////                sizeInBytes += i!.Inspection?.TotalSize ?? 0;
    ////            }
    ////        }
    ////        catch (Exception)
    ////        {
    ////        }

    ////        return (sizeInBytes / 1024 / 1024).ToString("0.0 MB");
    ////    }
    ////}

    /// <inheritdoc/>
    public void Dispose()
    {
        this.tokenSource?.Cancel();

        if (this.hubService is IDisposable d)
        {
            d.Dispose();
        }

        if (this.Inspections is not null)
        {
            foreach (var i in this.Inspections.Select(item => item as DownloadInspectionHandler))
            {
                i?.Dispose();
            }

            while (this.Inspections.Count > 0)
            {
                this.Inspections.RemoveAt(0);
            }

            this.Inspections.CollectionChanged -= this.Inspections_CollectionChanged;
        }
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
        this.Dispose();
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
        this.logger?.LogDebug($"New download requested: {parameter}");

        if (parameter is not Uri uri)
        {
            App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
            {
                if (this.navigationService.CanGoBack)
                {
                    this.navigationService.GoBack();
                }
                else
                {
                    this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!);
                }
            });

            return;
        }

        this.manifestUri = uri;

        this.tokenSource?.Cancel();
        this.tokenSource = new();
        _ = this.LoadManifest(this.tokenSource.Token);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.TotalCount):
                this.RaisePropertyChanged(nameof(this.InspectionCountString));
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            case nameof(this.DataFolder):
            case nameof(this.ManifestLoading):
                if (this.DownloadCommand is IRelayCommand rc)
                {
                    rc.NotifyCanExecuteChanged();
                }

                break;
            default:
                break;
        }
    }

    private async Task LoadManifest(CancellationToken token = default)
    {
        try
        {
            this.ManifestLoading = true;

            if (this.manifestUri is null)
            {
                this.logger?.LogError($"No uri. {this.manifestUri}");
                return;
            }

            var query = System.Web.HttpUtility.ParseQueryString(this.manifestUri.Query);
            var g = Guid.Empty;

            if (query is null)
            {
                this.logger?.LogError($"No parameters in uri. {this.manifestUri}");
                return;
            }

            Guid.TryParse(query.Get("id"), out g);
            this.DownloadName = query.Get("name");
            if (int.TryParse(query.Get("count"), out var count))
            {
                this.TotalCount = count;
            }

            bool.TryParse(query.Get("secure"), out var secure);

            if (token.IsCancellationRequested)
            {
                return;
            }

            // Make the call
            var host = @"https://api.pipetechproject.com";
#if DEBUG
            host = "http://localhost:5000";
#endif
            if (!string.IsNullOrEmpty(this.manifestUri.Authority))
            {
                if (!secure)
                {
                    host = "http://" + this.manifestUri.Authority;
                }
                else
                {
                    host = "https://" + this.manifestUri.Authority;
                }
            }

            this.hubService.SetBaseAddress(new Uri(host));

#if SLOWDOWN
            await Task.Delay(10000, token);
#endif

            if (token.IsCancellationRequested)
            {
                return;
            }

            var manifestUri = default(Uri?);
            try
            {
                manifestUri = await this.hubService.GetManifestLink(
                    g,
                    new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
            }
            catch (TaskCanceledException)
            {
                this.logger?.LogWarning($"Cancelled getting the manifest link [{host}][{g}]");
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, $"Error getting the manifest link [{host}][{g}]");
            }

#if SLOWDOWN
            await Task.Delay(10000, token);
#endif

            if (token.IsCancellationRequested)
            {
                return;
            }

            if (manifestUri is not null)
            {
                try
                {
                    this.Manifest = await this.hubService.GetManifest(
                        manifestUri,
                        new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token);
                }
                catch (TaskCanceledException)
                {
                    this.logger?.LogWarning($"Cancelled getting the manifest via link [{manifestUri}]");
                }
                catch (Exception ex)
                {
                    this.logger?.LogError(ex, $"Error getting the manifest via link [{manifestUri}]");
                }
            }

            if (token.IsCancellationRequested)
            {
                return;
            }

            try
            {
                this.Manifest ??= await this.hubService.GetManifest(
                        g,
                        new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token);
            }
            catch (TaskCanceledException)
            {
                this.logger?.LogWarning($"Cancelled getting the manifest via id [{host}][{g}]");
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex, $"Error getting the manifest via id [{host}][{g}]");
            }

#if SLOWDOWN
            await Task.Delay(10000, token);
#endif

            if (token.IsCancellationRequested)
            {
                return;
            }

            if (this.Manifest is not null)
            {
                if (this.Manifest.Id == Guid.Empty)
                {
                    this.Manifest.Id = g;
                }

                if (!string.IsNullOrEmpty(this.Manifest.DeliverableName))
                {
                    await App.MainWindow.DispatcherQueue.EnqueueAsync(() =>
                    {
                        this.DownloadName = this.Manifest.DeliverableName;
                    });
                }

                if (this.Manifest.Inspections is not null)
                {
                    var currentCount = 0;
                    foreach (var ele in this.Manifest.Inspections)
                    {
                        await App.MainWindow.DispatcherQueue.EnqueueAsync(
                            () =>
                            {
                                var dlh = this.serviceProvider.GetService(typeof(DownloadInspectionHandler)) as DownloadInspectionHandler;
                                dlh!.Inspection = new()
                                {
                                    Name = $"Inspection {++currentCount}",
                                    Project = this.DownloadName,
                                    Json = ele,
                                };

                                this.Inspections.Add(dlh);
                            });
                    }

                    if (this.Manifest.Inspections.Any())
                    {
                        if (this.Manifest.CombinedReportIds?.Any() == true)
                        {
                            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                                () =>
                                {
                                    this.Inspections.Add($"{(this.Manifest.DeliverableName ?? this.DownloadName)?.SanitizeFilename() ?? "Project report"}.pdf");
                                });
                        }

                        if (this.Manifest.CombinedNASSCOExchangeGenerate == true)
                        {
                            await App.MainWindow.DispatcherQueue.EnqueueAsync(
                                () =>
                                {
                                    this.Inspections.Add($"{(this.Manifest.DeliverableName ?? this.DownloadName)?.SanitizeFilename() ?? "NASSCO Exchange"}.mdb");
                                });
                        }
                    }
                }
            }
            else
            {
                this.logger?.LogWarning("No manifest received");
            }
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Error opening download link {this.manifestUri}");
        }
        finally
        {
            this.ManifestLoading = false;
            await App.MainWindow.DispatcherQueue.EnqueueAsync(() => { }, DispatcherQueuePriority.Normal);
        }
    }

    private void Inspections_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var item in e.OldItems)
            {
                if (item is DownloadInspectionHandler dlh)
                {
                    dlh.PropertyChanged -= this.InspectionHandler_PropertyChanged;
                    dlh.PropertyChanging -= this.InspectionHandler_PropertyChanging;

                    if (dlh.Inspection is not null)
                    {
                        dlh.Inspection.PropertyChanged -= this.Inspection_PropertyChanged;
                        dlh.Inspection.Files.CollectionChanged -= this.Files_CollectionChanged;
                    }
                }
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var item in e.NewItems)
            {
                if (item is DownloadInspectionHandler dlh)
                {
                    dlh.PropertyChanged += this.InspectionHandler_PropertyChanged;
                    dlh.PropertyChanging += this.InspectionHandler_PropertyChanging;

                    if (dlh.Inspection is not null)
                    {
                        dlh.Inspection.PropertyChanged += this.Inspection_PropertyChanged;
                        dlh.Inspection.Files.CollectionChanged += this.Files_CollectionChanged;
                    }
                }
            }
        }

        this.RaisePropertyChanged(nameof(this.InspectionCountString));
        this.RaisePropertyChanged(nameof(this.TotalSize));
    }

    private void Files_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var item in e.OldItems)
            {
                if (item is Models.File f)
                {
                    f.PropertyChanged -= this.File_PropertyChanged;
                }
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var item in e.NewItems)
            {
                if (item is Models.File f)
                {
                    f.PropertyChanged += this.File_PropertyChanged;
                }
            }
        }

        this.RaisePropertyChanged(nameof(this.TotalSize));
    }

    private void File_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Models.File.Size):
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            default:
                break;
        }
    }

    private void InspectionHandler_PropertyChanging(object? sender, PropertyChangingEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (sender is DownloadInspectionHandler dlh &&
                    dlh.Inspection is not null)
                {
                    dlh.Inspection.PropertyChanged -= this.Inspection_PropertyChanged;
                }

                break;
            default:
                break;
        }
    }

    private void InspectionHandler_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (sender is DownloadInspectionHandler dlh &&
                    dlh.Inspection is not null)
                {
                    dlh.Inspection.PropertyChanged += this.Inspection_PropertyChanged;
                }

                this.RaisePropertyChanged(nameof(this.InspectionCountString));
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            default:
                break;
        }
    }

    private void Inspection_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspection.TotalSize):
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            case nameof(DownloadInspection.Files):
                break;
            case nameof(DownloadInspection.State):
                if (this.DownloadCommand is IRelayCommand rc)
                {
                    rc.NotifyCanExecuteChanged();
                }

                break;
            default:
                break;
        }
    }

    private void ExecuteBrowseDataFolder()
    {
        var d = new CommonOpenFileDialog()
        {
            IsFolderPicker = true,
            Title = "Select Download Folder",
        };

        if (d.ShowDialog() == CommonFileDialogResult.Ok)
        {
            this.DataFolder = d.FileName;
        }
    }

    private bool CanExecuteDownload()
    {
        return !this.ManifestLoading &&
            this.Inspections is not null &&
            this.Inspections.Any() == true &&
            !this.Inspections
            .Select(i => i as DownloadInspectionHandler)
            .Where(i => i is not null)
            .Any(i => i!.Inspection?.State == DownloadInspection.States.Loading ||
            i.Inspection?.State == DownloadInspection.States.Errored ||
            i.Inspection?.State == DownloadInspection.States.Paused) &&
            !string.IsNullOrEmpty(this.DataFolder) &&
            Directory.Exists(this.DataFolder);
    }

    private async Task ExecuteDownload()
    {
        try
        {
            if (this.UseDefault)
            {
                await this.localSettingsService.SaveSettingAsync(LASTDATAFOLDERSETTING, this.DataFolder);
            }

            foreach (var item in this.Inspections.Select(i => i as DownloadInspectionHandler).Where(i => i is not null))
            {
                if (item?.Inspection is not null)
                {
                    item.Inspection.DownloadPath = Path.Combine(this.DataFolder!, item.Inspection.Name ?? "Inspection");
                    if (item.Inspection.Files is not null)
                    {
                        foreach (var f in item.Inspection.Files)
                        {
                            f.DownloadPath = Path.Combine(".\\", Path.GetFileName(Uri.UnescapeDataString(f.Name)));
                        }
                    }
                }
            }

            var dir = Path.Combine(
                this.paths.MachineSettingsDirectory,
                DateTime.Now.ToString("s").SanitizeFilename() ?? DateTime.Now.ToString("yyyy-MM-dd HHmmss"));

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var p = Project.FromJson(string.Empty);
            p.Id = Guid.NewGuid();
            p.Name = this.DownloadName;
            p.ConfirmationTime = DateTime.Now;
            p.DownloadPath = this.DataFolder;
            p.Inspections ??= new();
            p.Inspections!.AddRange(
                this.Inspections
                .Select(i => i as DownloadInspectionHandler)
                .Where(i => i is not null));
            if (p is IManifest m)
            {
                m.AdditionalProperties = this.Manifest?.AdditionalProperties;
                m.IndividualReportIds = this.Manifest?.IndividualReportIds;
                m.CombinedNASSCOExchangeGenerate = this.Manifest?.CombinedNASSCOExchangeGenerate;
                m.DeliverableName = this.Manifest?.DeliverableName;
                m.IndividualNASSCOExchangeGenerate = this.Manifest?.IndividualNASSCOExchangeGenerate;
                m.CombinedReportIds = this.Manifest?.CombinedReportIds;
                m.IndividualReportIds = this.Manifest?.IndividualReportIds;
            }

            var data = p.ToJson();
            System.IO.File.WriteAllText(Path.Combine(dir, "info.json"), data);

            this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!, clearNavigation: true);
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error initiating download.");
        }
    }
}
