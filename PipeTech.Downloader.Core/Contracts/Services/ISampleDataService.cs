using PipeTech.Downloader.Core.Models;

namespace PipeTech.Downloader.Core.Contracts.Services;

// Remove this class once your pages/features are using your data.
public interface ISampleDataService
{
    Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync();

    Task<IEnumerable<SampleOrder>> GetGridDataAsync();
}
