// <copyright file="DownloadsViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;

using PipeTech.Downloader.Contracts.ViewModels;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Downloads view model.
/// </summary>
public partial class DownloadsViewModel : ObservableRecipient, INavigationAware
{
    ////private readonly ISampleDataService _sampleDataService;

    ////public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadsViewModel"/> class.
    /// </summary>
    public DownloadsViewModel() // ISampleDataService sampleDataService)
    {
        ////_sampleDataService = sampleDataService;
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
        ////Source.Clear();

        ////// TODO: Replace with real data.
        ////var data = await _sampleDataService.GetGridDataAsync();

        ////foreach (var item in data)
        ////{
        ////    Source.Add(item);
        ////}
    }

    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }
}
