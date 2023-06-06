using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Core.Contracts.Services;
using PipeTech.Downloader.Core.Models;

namespace PipeTech.Downloader.ViewModels;

public partial class DownloadsViewModel : ObservableRecipient, INavigationAware
{
    private readonly ISampleDataService _sampleDataService;

    public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

    public DownloadsViewModel(ISampleDataService sampleDataService)
    {
        _sampleDataService = sampleDataService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();

        // TODO: Replace with real data.
        var data = await _sampleDataService.GetGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
