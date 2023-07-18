// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI.Controls;
using Hangfire;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;
using Windows.System;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Downloads view model.
/// </summary>
public partial class DownloadsViewModel : BindableRecipient, INavigationAware
{
    private readonly IServiceProvider serviceProvider;
    private readonly IDownloadService downloadService;
    private readonly ILogger<DownloadsViewModel>? logger;
    private readonly IBackgroundJobClientV2 jobClient;
    private readonly IThemeSelectorService themeSelectorService;

    private bool expanding = false;
    private ElementTheme requestedTheme = ElementTheme.Default;
    private ContentDialog? dialog = null;

    /// <summary>
    /// Gets or sets the row visibility.
    /// </summary>
    [ObservableProperty]
    private DataGridRowDetailsVisibilityMode visibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadsViewModel"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service Provider.</param>
    /// <param name="options">Settings directories.</param>
    /// <param name="downloadService">Download service.</param>
    /// <param name="jobClient">Job client service.</param>
    /// <param name="themeSelectorService">Theme selector service.</param>
    /// <param name="logger">Logger service.</param>
    public DownloadsViewModel(
        IServiceProvider serviceProvider,
        IDownloadService downloadService,
        IBackgroundJobClientV2 jobClient,
        IThemeSelectorService themeSelectorService,
        ILogger<DownloadsViewModel>? logger = null)
    {
        this.serviceProvider = serviceProvider;
        this.downloadService = downloadService;
        this.jobClient = jobClient;
        this.themeSelectorService = themeSelectorService;
        this.logger = logger;

        this.OpenFolderCommand = new AsyncRelayCommand<object?>(this.ExecuteOpenFolder);
        this.RemoveProjectCommand = new AsyncRelayCommand<object?>(this.RemoveProject);
        this.RestartProjectDownloadCommand = new AsyncRelayCommand<object?>(this.ExecuteRestartProjectDownload);
        this.PauseProjectDownloadCommand = new AsyncRelayCommand<object?>(this.ExecutePauseProjectDownload);
        this.ShowDetailsCommand = new AsyncRelayCommand<object?>(this.ExecuteShowDetails);
        this.SourceView = CollectionViewSource.GetDefaultView(this.downloadService.Source);
    }

    /////// <summary>
    /////// Gets the source data.
    /////// </summary>
    ////public ObservableCollection<Project> Source => this.downloadService.Source;

    /// <summary>
    /// Gets the source view.
    /// </summary>
    public ICollectionView SourceView
    {
        get;
    }

    /// <summary>
    /// Gets the command to open the project download folder.
    /// </summary>
    public IAsyncRelayCommand<object?> OpenFolderCommand
    {
        get;
    }

    /// <summary>
    /// Gets the command to remove project.
    /// </summary>
    public IAsyncRelayCommand<object?> RemoveProjectCommand
    {
        get;
    }

    /// <summary>
    /// Gets the command to restart a project download.
    /// </summary>
    public IAsyncRelayCommand<object?> RestartProjectDownloadCommand
    {
        get;
    }

    /// <summary>
    /// Gets the command to pause a project download.
    /// </summary>
    public IAsyncRelayCommand<object?> PauseProjectDownloadCommand
    {
        get;
    }

    /// <summary>
    /// Gets the show details command.
    /// </summary>
    public IAsyncRelayCommand<object?> ShowDetailsCommand
    {
        get;
    }

    /// <summary>
    /// Gets or sets the requested theme.
    /// </summary>
    public ElementTheme RequestedTheme
    {
        get => this.requestedTheme;
        protected set => this.SetProperty(ref this.requestedTheme, value);
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
        this.RequestedTheme = this.themeSelectorService.Theme;
        var messenger = this.serviceProvider.GetService(typeof(IMessenger)) as IMessenger;
        messenger?.Register<ThemeChangedMessage>(this, (r, m) =>
        {
            this.RequestedTheme = m.Value;
        });

        this.downloadService.Source.CollectionChanged += this.Source_CollectionChanged;
        foreach (var p in this.downloadService.Source)
        {
            if (p is null)
            {
                continue;
            }

            p.PropertyChanged += this.Project_PropertyChanged;
        }

        if (this.downloadService.Source.FirstOrDefault(p => p.Expanded) is Project expandedProject)
        {
            expandedProject.Expanded = !expandedProject.Expanded;
            expandedProject.Expanded = !expandedProject.Expanded;
        }

        _ = this.downloadService.LoadDownloads(new CancellationTokenSource(TimeSpan.FromMinutes(10)).Token);
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
        this.dialog?.Hide();
        this.dialog = null;

        var messenger = this.serviceProvider.GetService(typeof(IMessenger)) as IMessenger;
        messenger?.Unregister<ThemeChangedMessage>(this);

        if (this.downloadService?.Source is not null)
        {
            this.downloadService.Source.CollectionChanged -= this.Source_CollectionChanged;
            foreach (var p in this.downloadService.Source)
            {
                if (p is null)
                {
                    continue;
                }

                p.PropertyChanged -= this.Project_PropertyChanged;
                p.Expanded = false;
            }
        }
    }

    private async Task ExecuteShowDetails(object? param)
    {
        if (param is not DownloadInspection dl ||
            string.IsNullOrEmpty(dl.LastError))
        {
            await Task.CompletedTask;
            return;
        }

        try
        {
            this.dialog?.Hide();

            this.dialog = new ContentDialog()
            {
                XamlRoot = App.MainWindow.Content.XamlRoot,
                Title = string.Empty,
                Content = dl.LastError,
                CloseButtonText = "Close",
            };

            await this.dialog.ShowAsync();
        }
        catch (Exception)
        {
        }
    }

    private async Task ExecuteOpenFolder(object? param)
    {
        if (param is not Project p ||
            string.IsNullOrEmpty(p.DownloadPath) ||
            !Directory.Exists(p.DownloadPath))
        {
            await Task.CompletedTask;
            return;
        }

        try
        {
            await Launcher.LaunchFolderPathAsync(p.DownloadPath);
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Unable to open folder [{p.DownloadPath}]");
        }
    }

    private async Task RemoveProject(object? param)
    {
        if (param is not Project p ||
            string.IsNullOrEmpty(p.FilePath) ||
            string.IsNullOrEmpty(Path.GetDirectoryName(p.FilePath)))
        {
            await Task.CompletedTask;
            return;
        }

        try
        {
            this.dialog?.Hide();

            this.dialog = new ContentDialog()
            {
                XamlRoot = App.MainWindow.Content.XamlRoot,
                Content = "Are you sure you want to remove the project download?\r\n\r\nNOTE: This operation will not remove the actual download",
                CloseButtonText = "No",
                PrimaryButtonText = "Yes",
                DefaultButton = ContentDialogButton.Close,
            };
            var result = await this.dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            this.downloadService.Source.Remove(p);

            var dir = new DirectoryInfo(Path.GetDirectoryName(p.FilePath) ?? string.Empty);
            if (dir.Exists)
            {
                dir.Delete(true);
            }
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Unable to remove project [{p.FilePath}]");
        }
    }

    private async Task ExecutePauseProjectDownload(object? param)
    {
        if (param is not Project p || p.Inspections is null)
        {
            return;
        }

        // Find jobs for the project and cancel them.
        var jobs = this.downloadService.FindJobForProject(p);
        while (jobs is not null && jobs.Any())
        {
            foreach (var j in jobs)
            {
                this.jobClient.Delete(j.Key);
            }

            await Task.Delay(500);
            jobs = this.downloadService.FindJobForProject(p);
        }

        // Set the inspection state of each non-completed inspection to staged
        foreach (var i in p.Inspections)
        {
            if (i.Inspection is not null &&
            i.Inspection.State != DownloadInspection.States.Complete)
            {
                i.Inspection.State = DownloadInspection.States.Paused;
            }
        }
    }

    private async Task ExecuteRestartProjectDownload(object? param)
    {
        if (param is not Project p || p.Inspections is null)
        {
            return;
        }

        // Find jobs for the project and cancel them.
        var jobs = this.downloadService.FindJobForProject(p);
        while (jobs is not null && jobs.Any())
        {
            foreach (var j in jobs)
            {
                this.jobClient.Delete(j.Key);
            }

            await Task.Delay(500);
            jobs = this.downloadService.FindJobForProject(p);
        }

        // Set the inspection state of each non-completed inspection to staged
        foreach (var i in p.Inspections)
        {
            if (i.Inspection is not null &&
            i.Inspection.State != DownloadInspection.States.Complete)
            {
                i.Inspection.State = DownloadInspection.States.Staged;
            }
        }

        // Create a job
        await this.downloadService.CreateJobForProject(p);
    }

    private void Source_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var oldItem in e.OldItems)
            {
                if (oldItem is not Project p)
                {
                    continue;
                }

                p.PropertyChanged -= this.Project_PropertyChanged;
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var newItem in e.NewItems)
            {
                if (newItem is not Project p)
                {
                    continue;
                }

                p.PropertyChanged += this.Project_PropertyChanged;
            }
        }
    }

    private void Project_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Project.Expanded):
                if (sender is Project p)
                {
                    if (p.Expanded)
                    {
                        this.expanding = true;
                        try
                        {
                            foreach (var project in this.downloadService.Source)
                            {
                                if (project is null)
                                {
                                    continue;
                                }

                                if (!project.Equals(p))
                                {
                                    project.Expanded = false;
                                }
                            }

                            this.VisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
                            this.SourceView.MoveCurrentTo(p);
                            this.RaisePropertyChanged(nameof(this.RequestedTheme));
                        }
                        catch (Exception ex)
                        {
                            this.logger?.LogError(ex, "Error expanding");
                        }
                        finally
                        {
                            this.expanding = false;
                        }
                    }
                    else if (!this.expanding)
                    {
                        this.VisibilityMode = DataGridRowDetailsVisibilityMode.Collapsed;
                        this.SourceView.MoveCurrentTo(null);
                        this.RaisePropertyChanged(nameof(this.RequestedTheme));
                    }
                }

                break;
            default:
                break;
        }
    }
}
