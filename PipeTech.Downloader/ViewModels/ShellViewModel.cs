// <copyright file="ShellViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.Models;
using PipeTech.Downloader.Views;
using Windows.UI.ViewManagement;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Shell view model class.
/// </summary>
public partial class ShellViewModel : BindableRecipient
{
    private readonly SvgImageSource? lightImageSource;
    private readonly SvgImageSource? darkImageSource;

    private bool isClosing = false;

    ////[ObservableProperty]
    ////private bool isBackEnabled;

    [ObservableProperty]
    private bool canSettings;

    [ObservableProperty]
    private ElementTheme currentTheme;

    /// <summary>
    /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="notificationService">Notification service.</param>
    public ShellViewModel(
        INavigationService navigationService,
        IAppNotificationService notificationService)
    {
        this.NavigationService = navigationService;
        this.NavigationService.Navigated += this.OnNavigated;

        this.NotificationService = notificationService;

        this.MenuFileExitCommand = new RelayCommand(this.OnMenuFileExit);
        this.MenuViewsDownloadsCommand = new RelayCommand(this.OnMenuViewsDownloads);
        this.MenuSettingsCommand = new RelayCommand(this.OnMenuSettings, this.CanMenuSettings);
        this.MenuViewsMainCommand = new RelayCommand(this.OnMenuViewsMain);
        this.ShowCommand = new RelayCommand(this.OnShow);

        try
        {
            this.lightImageSource = new SvgImageSource(new Uri(@"ms-appx:///Assets/LightLogo.svg", UriKind.RelativeOrAbsolute));
            this.darkImageSource = new SvgImageSource(new Uri(@"ms-appx:///Assets/Logo.svg", UriKind.RelativeOrAbsolute));
        }
        catch (Exception)
        {
        }

        App.MainWindow.AppWindow.Closing += this.AppWindow_Closing;
    }

    /// <summary>
    /// Gets the the current svg source.
    /// </summary>
    public SvgImageSource? CurrentSVGSource
    {
        get
        {
            var theme = this.CurrentTheme;
            if (theme == ElementTheme.Default)
            {
                var uiSettings = new UISettings();
                var background = uiSettings.GetColorValue(UIColorType.Background);

                theme = background == Colors.White ? ElementTheme.Light : ElementTheme.Dark;
            }

            return theme == ElementTheme.Light ?
                this.lightImageSource :
                this.darkImageSource;
        }
    }

    /// <summary>
    /// Gets the show command.
    /// </summary>
    public ICommand ShowCommand
    {
        get;
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

    /// <summary>
    /// Gets the notification service.
    /// </summary>
    public IAppNotificationService NotificationService
    {
        get;
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.CurrentTheme):
                this.RaisePropertyChanged(nameof(this.CurrentSVGSource));
                break;
            default:
                break;
        }
    }

    private void AppWindow_Closing(
        Microsoft.UI.Windowing.AppWindow sender,
        Microsoft.UI.Windowing.AppWindowClosingEventArgs args)
    {
        if (!this.isClosing)
        {
            args.Cancel = true;
            App.MainWindow.Hide(true);

            Task.Run(async () =>
            {
                try
                {
                    var settingsService = App.GetService<ILocalSettingsService>();
                    if (await settingsService.ReadSettingAsync<bool>(ILocalSettingsService.CloseNotificationShownKey) != true)
                    {
                        this.NotificationService?.Show(
                            "ClosingToTrayNotificationPayload".GetLocalized(),
                            TimeSpan.FromSeconds(10));
                        _ = settingsService.SaveSettingAsync(ILocalSettingsService.CloseNotificationShownKey, true);
                    }
                }
                catch (Exception)
                {
                }
            });

            return;
        }

        App.MainWindow.AppWindow.Closing -= this.AppWindow_Closing;
        App.Current.Exit();
    }

    private void OnShow()
    {
        App.MainWindow.Show(true);
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        ////this.IsBackEnabled = this.NavigationService.CanGoBack;
        this.CanSettings = e.Content is not MainPage;
        if (this.MenuSettingsCommand is IRelayCommand rc)
        {
            rc.NotifyCanExecuteChanged();
        }
    }

    private void OnMenuFileExit()
    {
        this.isClosing = true;
        App.MainWindow.Close();
        ////Application.Current.Exit();
    }

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
