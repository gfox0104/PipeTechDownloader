using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

using PipeTech.Downloader.Contracts.Services;

namespace PipeTech.Downloader.ViewModels;

public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    public ICommand MenuFileExitCommand
    {
        get;
    }

    public ICommand MenuViewsDownloadsCommand
    {
        get;
    }

    public ICommand MenuSettingsCommand
    {
        get;
    }

    public ICommand MenuViewsListDetailsCommand
    {
        get;
    }

    public ICommand MenuViewsMainCommand
    {
        get;
    }

    public INavigationService NavigationService
    {
        get;
    }

    public ShellViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;

        MenuFileExitCommand = new RelayCommand(OnMenuFileExit);
        MenuViewsDownloadsCommand = new RelayCommand(OnMenuViewsDownloads);
        MenuSettingsCommand = new RelayCommand(OnMenuSettings);
        MenuViewsListDetailsCommand = new RelayCommand(OnMenuViewsListDetails);
        MenuViewsMainCommand = new RelayCommand(OnMenuViewsMain);
    }

    private void OnNavigated(object sender, NavigationEventArgs e) => IsBackEnabled = NavigationService.CanGoBack;

    private void OnMenuFileExit() => Application.Current.Exit();

    private void OnMenuViewsDownloads() => NavigationService.NavigateTo(typeof(DownloadsViewModel).FullName!);

    private void OnMenuSettings() => NavigationService.NavigateTo(typeof(SettingsViewModel).FullName!);

    private void OnMenuViewsListDetails() => NavigationService.NavigateTo(typeof(ListDetailsViewModel).FullName!);

    private void OnMenuViewsMain() => NavigationService.NavigateTo(typeof(MainViewModel).FullName!);
}
