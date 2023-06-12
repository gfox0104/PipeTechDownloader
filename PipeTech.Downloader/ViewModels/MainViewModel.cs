// <copyright file="MainViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAPICodePack.Dialogs;
using MvvmHelpers.Commands;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Main view model class.
/// </summary>
public partial class MainViewModel : BindableRecipient, INavigationAware
{
    private static readonly string LASTDATAFOLDERSETTING = "LastDataFolder";

    private readonly INavigationService navigationService;
    private readonly IHubService hubService;
    private readonly ILogger<MainViewModel>? logger;
    private readonly ILocalSettingsService localSettingsService;
    private readonly IServiceProvider serviceProvider;

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

    /// <summary>
    /// Initializes a new instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="hubService">Hub service.</param>
    /// <param name="serviceProvider">Service provider.</param>
    /// <param name="localSettingsService">Local settings service.</param>
    /// <param name="logger">Logger service.</param>
    public MainViewModel(
        INavigationService navigationService,
        IHubService hubService,
        IServiceProvider serviceProvider,
        ILocalSettingsService localSettingsService,
        ILogger<MainViewModel>? logger = null)
    {
        this.navigationService = navigationService;
        this.hubService = hubService;
        this.serviceProvider = serviceProvider;
        this.localSettingsService = localSettingsService;
        this.logger = logger;

        this.Inspections = new ObservableCollection<DownloadInspection>();
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
    /// Gets the inspections.
    /// </summary>
    public ObservableCollection<DownloadInspection> Inspections
    {
        get;
    }

    /// <summary>
    /// Gets the inspection count string.
    /// </summary>
    public string? InspectionCountString
    {
        get
        {
            var value = $"{this.Inspections.Count()}";
            if (this.TotalCount is int totalCount)
            {
                return value + $" of {totalCount}";
            }

            return value;
        }
    }

    /// <summary>
    /// Gets the total size string in MB.
    /// </summary>
    public string? TotalSizeinMB
    {
        get
        {
            var sizeInBytes = 0L;
            try
            {
                foreach (var i in this.Inspections)
                {
                    sizeInBytes += i.TotalSize ?? 0;
                }
            }
            catch (Exception)
            {
            }

            return (sizeInBytes / 1024 / 1024).ToString("0.0 MB");
        }
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }

    /// <inheritdoc/>
    public async void OnNavigatedTo(object parameter)
    {
        try
        {
            this.logger?.LogDebug($"New download requested: {parameter}");

            if (parameter is not Uri uri)
            {
                if (this.navigationService.CanGoBack)
                {
                    this.navigationService.GoBack();
                }
                else
                {
                    this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!);
                }

                return;
            }

            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            var g = Guid.Empty;

            if (query is null)
            {
                this.logger?.LogError($"No parameter in uri. {uri}");
                return;
            }

            Guid.TryParse(query.Get("id"), out g);
            this.DownloadName = query.Get("name");
            if (int.TryParse(query.Get("count"), out var count))
            {
                this.TotalCount = count;
            }

            bool.TryParse(query.Get("secure"), out var secure);

            // Make the call
            var host = @"https://api.pipetechproject.com";
#if DEBUG
            host = "http://localhost:5000";
#endif
            if (!string.IsNullOrEmpty(uri.Authority))
            {
                if (!secure)
                {
                    host = "http://" + uri.Authority;
                }
                else
                {
                    host = "https://" + uri.Authority;
                }
            }

            this.hubService.SetBaseAddress(new Uri(host));

            var manifest = await this.hubService.GetManifest(g, new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);

            if (manifest is not null)
            {
                if (!string.IsNullOrEmpty(manifest.DeliverableName))
                {
                    this.DownloadName = manifest.DeliverableName;
                }

                if (manifest.Inspections is not null)
                {
                    var currentCount = 0;
                    foreach (var ele in manifest.Inspections)
                    {
                        this.Inspections.Add(new(this.serviceProvider)
                        {
                            Name = $"Inspection {++currentCount}",
                            State = DownloadInspection.States.Loading,
                            Json = ele.ToString(),
                        });
                    }
                }
            }
            else
            {
                this.logger?.LogWarning("No manifest received");
            }
#if DEBUG
            // Fake some data
            if (this.Inspections.Count <= 0)
            {
                this.Inspections.Add(new(this.serviceProvider)
                {
                    Name = "name",
                    State = DownloadInspection.States.Loading,
                    TotalSize = 0,
                });
            }
#endif

            this.ManifestLoading = false;
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Error opening download link {parameter}");
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.TotalCount):
                this.RaisePropertyChanged(nameof(this.InspectionCountString));
                this.RaisePropertyChanged(nameof(this.TotalSizeinMB));
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

    private void Inspections_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.OldItems)
            {
                item.PropertyChanged -= this.Inspection_PropertyChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (INotifyPropertyChanged item in e.NewItems)
            {
                item.PropertyChanged += this.Inspection_PropertyChanged;
            }
        }

        this.RaisePropertyChanged(nameof(this.InspectionCountString));
        this.RaisePropertyChanged(nameof(this.TotalSizeinMB));
    }

    private void Inspection_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspection.TotalSize):
                App.MainWindow.DispatcherQueue.TryEnqueue(() =>
                {
                    this.RaisePropertyChanged(nameof(this.TotalSizeinMB));
                });
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

            this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!, clearNavigation: true);
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error initiating download.");
        }
    }
}
