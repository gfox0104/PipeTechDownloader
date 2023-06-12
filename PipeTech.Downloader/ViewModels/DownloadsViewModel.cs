// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Downloads view model.
/// </summary>
public partial class DownloadsViewModel : ObservableRecipient, INavigationAware
{
    private readonly IServiceProvider serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadsViewModel"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service Provider.</param>
    public DownloadsViewModel(
        IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;

        this.Source = new ObservableCollection<DownloadInspection>();

#if DEBUG
        if (this.Source.Count <= 0)
        {
            this.Source.Add(new(this.serviceProvider)
            {
                DownloadPath = "Here",
                Name = "name here",
                Project = "proj name",
                Size = 123,
                TotalSize = 1234,
                Progress = 0.2m,
                State = DownloadInspection.States.Complete,
            });
        }
#endif
    }

    /// <summary>
    /// Gets the source data.
    /// </summary>
    public ObservableCollection<DownloadInspection> Source
    {
        get;
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }
}
