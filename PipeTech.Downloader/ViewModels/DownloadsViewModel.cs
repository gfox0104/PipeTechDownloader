// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.UI.Dispatching;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;
using PT.Inspection;

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
    }

    /// <summary>
    /// Gets the source data.
    /// </summary>
    public ObservableCollection<Project> Source => this.downloadService.Source;

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
