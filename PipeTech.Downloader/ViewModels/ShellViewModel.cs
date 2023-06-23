// <copyright file="ShellViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Navigation;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Views;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Shell view model class.
/// </summary>
public partial class ShellViewModel : ObservableRecipient
{
    [ObservableProperty]
    private bool isBackEnabled;

    [ObservableProperty]
    private bool canSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public ShellViewModel(INavigationService navigationService)
    {
        this.NavigationService = navigationService;
        this.NavigationService.Navigated += this.OnNavigated;

        this.MenuFileExitCommand = new RelayCommand(this.OnMenuFileExit);
        this.MenuViewsDownloadsCommand = new RelayCommand(this.OnMenuViewsDownloads);
        this.MenuSettingsCommand = new RelayCommand(this.OnMenuSettings, this.CanMenuSettings);
        this.MenuViewsMainCommand = new RelayCommand(this.OnMenuViewsMain);
    }

    /// <summary>
    /// Gets the file exit menu command.
    /// </summary>
    public ICommand MenuFileExitCommand
    {
        get;
    }

    /// <summary>
    /// Gets the View downloads menu command.
    /// </summary>
    public ICommand MenuViewsDownloadsCommand
    {
        get;
    }

    /// <summary>
    /// Gets the settings menu command.
    /// </summary>
    public ICommand MenuSettingsCommand
    {
        get;
    }

    /// <summary>
    /// Gets the View main menu command.
    /// </summary>
    public ICommand MenuViewsMainCommand
    {
        get;
    }

    /// <summary>
    /// Gets the navigation service.
    /// </summary>
    public INavigationService NavigationService
    {
        get;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        this.IsBackEnabled = this.NavigationService.CanGoBack;
        this.CanSettings = e.Content is not MainPage;
        if (this.MenuSettingsCommand is IRelayCommand rc)
        {
            rc.NotifyCanExecuteChanged();
        }
    }

    private void OnMenuFileExit() => Application.Current.Exit();

    private void OnMenuViewsDownloads() => this.NavigationService.NavigateTo(typeof(DownloadsViewModel).FullName!);

    private bool CanMenuSettings()
    {
        return this.CanSettings;
    }

    private void OnMenuSettings()
    {
        if (this.NavigationService.Frame?.Content?.GetType() == typeof(SettingsPage))
        {
            this.NavigationService.GoBack();
        }
        else
        {
            this.NavigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        }
    }

    private void OnMenuViewsMain() => this.NavigationService.NavigateTo(typeof(MainViewModel).FullName!);
}
