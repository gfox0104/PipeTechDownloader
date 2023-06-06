// <copyright file="App.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;

using PipeTech.Downloader.Activation;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Core.Contracts.Services;
using PipeTech.Downloader.Core.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.Models;
using PipeTech.Downloader.Notifications;
using PipeTech.Downloader.Services;
using PipeTech.Downloader.ViewModels;
using PipeTech.Downloader.Views;

namespace PipeTech.Downloader;

/// <summary>
/// Application class.
/// </summary>
/// <remarks>To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.</remarks>
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging

    /// <summary>
    /// Initializes a new instance of the <see cref="App"/> class.
    /// </summary>
    public App()
    {
        this.InitializeComponent();

        var keyInstance = AppInstance.GetCurrent();
        if (keyInstance is not null && keyInstance.IsCurrent)
        {
            keyInstance.Activated += this.OnActivated;
        }

        this.Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers
                services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();

                // Services
                services.AddSingleton<IAppNotificationService, AppNotificationService>();
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();

                // Core Services
                services.AddSingleton<ISampleDataService, SampleDataService>();
                services.AddSingleton<IFileService, FileService>();

                // Views and ViewModels
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<ListDetailsViewModel>();
                services.AddTransient<ListDetailsPage>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainPage>();
                services.AddTransient<ShellPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<DownloadsViewModel>();
                services.AddTransient<DownloadsPage>();

                // Configuration
                services.Configure<LocalSettingsOptions>(
                    context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            })
            .Build();

        App.GetService<IAppNotificationService>().Initialize();

        this.UnhandledException += this.App_UnhandledException;
    }

    /// <summary>
    /// Gets the Main window.
    /// </summary>
    public static WindowEx MainWindow { get; } = new MainWindow();

    /// <summary>
    /// Gets or sets the application title bar element.
    /// </summary>
    public static UIElement? AppTitlebar
    {
        get; set;
    }

    /// <summary>
    /// Gets the host.
    /// </summary>
    public IHost Host
    {
        get;
    }

    /// <summary>
    /// Get a service from the application's host.
    /// </summary>
    /// <typeparam name="T">Type of service to get.</typeparam>
    /// <returns>Service object.</returns>
    /// <exception cref="ArgumentException">No service is registered of the type requested.</exception>
    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered.");
        }

        return service;
    }

    /// <summary>
    /// Callback for when the application is launched.
    /// </summary>
    /// <param name="args">Arguments.</param>
    protected async override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        ////App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    private void OnActivated(object? sender, AppActivationArguments args)
    {
        if (args.Kind == ExtendedActivationKind.Protocol)
        {
            if (args.Data is Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs cool)
            {
                ////GetService<IAppNotificationService>().Show(string.Format("OpenedSomething".GetLocalized(), AppContext.BaseDirectory, cool.Uri.AbsoluteUri));
                App.MainWindow.BringToFront();
            }
        }
    }
}
