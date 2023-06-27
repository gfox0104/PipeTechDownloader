// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Extensions.Logging;
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

        this.SourceView = CollectionViewSource.GetDefaultView(this.Source);
    }

    /// <summary>
    /// Gets the source data.
    /// </summary>
    public ObservableCollection<Project> Source => this.downloadService.Source;

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
        _ = this.downloadService.LoadDownloads(new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token);
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }
}
