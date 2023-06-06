using Microsoft.UI.Xaml.Controls;

using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

// TODO: Change the grid as appropriate for your app. Adjust the column definitions on DataGridPage.xaml.
// For more details, see the documentation at https://docs.microsoft.com/windows/communitytoolkit/controls/datagrid.
public sealed partial class DownloadsPage : Page
{
    public DownloadsViewModel ViewModel
    {
        get;
    }

    public DownloadsPage()
    {
        ViewModel = App.GetService<DownloadsViewModel>();
        InitializeComponent();
    }
}
