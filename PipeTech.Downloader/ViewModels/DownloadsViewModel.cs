// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Extensions.Logging;
using NPOI.SS.Formula.Functions;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Downloads view model.
/// </summary>
public partial class DownloadsViewModel : ObservableRecipient, INavigationAware
{
    private readonly IServiceProvider serviceProvider;
    private readonly IDownloadService downloadService;
    private readonly ILogger<DownloadsViewModel>? logger;

    private bool expanding = false;

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
    /// <param name="logger">Logger service.</param>
    public DownloadsViewModel(
        IServiceProvider serviceProvider,
        IDownloadService downloadService,
        ILogger<DownloadsViewModel>? logger = null)
    {
        this.serviceProvider = serviceProvider;
        this.downloadService = downloadService;
        this.logger = logger;

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

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
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

        _ = this.downloadService.LoadDownloads(new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token);
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
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
                    }
                }

                break;
            default:
                break;
        }
    }
}
