using Microsoft.UI.Xaml.Controls;

using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
