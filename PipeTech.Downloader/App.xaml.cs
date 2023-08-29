// <copyright file="App.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Net;
using System.Reflection;
using CommunityToolkit.Mvvm.Messaging;
using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using PipeTech.Downloader.Activation;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Core.Contracts.Services;
using PipeTech.Downloader.Core.Services;
using PipeTech.Downloader.Models;
using PipeTech.Downloader.Notifications;
using PipeTech.Downloader.Services;
using PipeTech.Downloader.ViewModels;
using PipeTech.Downloader.Views;
using Prism.Ioc;
using PT.Inspection;
using PT.Inspection.Inspections;
using PT.Inspection.LogicRegistry;
using PT.Inspection.Model;
using PT.Inspection.Packs;
using PT.Inspection.Reporting;
using PT.Inspection.Reporting.Default;
using PT.Inspection.Reporting.Syncfusion;
using PT.Inspection.Sewer.NASSCO.Logic;
using PT.Inspection.Sewer.WSA.Logic;
using PT.Inspection.Templates;
using PT.Inspection.Wpf.Logic;
using Serilog;
using Serilog.Settings.Configuration;
using Syncfusion.Licensing;

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

        try
        {
            var extraAllowedTypes = new Type[]
            {
                typeof(PT.Model.DataTypes.Clock),
            };

            AppDomain.CurrentDomain.SetData("System.Data.DataSetDefaultAllowedTypes", extraAllowedTypes);
        }
        catch (Exception)
        {
        }

        this.Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
#if DEBUG
            .UseEnvironment("Development")
#endif
            .ConfigureWebHost(b =>
            {
                b.UseKestrel((c, o) =>
                {
                })
                .UseUrls("http://*:9000")
                .ConfigureServices(s =>
                {
                    s.AddHangfire((sp, c) =>
                    {
                        c
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UseSQLiteStorage(
                            "pipetech_downloader_jobs.db",
                            new SQLiteStorageOptions()
                            {
                                InvisibilityTimeout = TimeSpan.FromHours(2),
                            })
                        .UseActivator(new ServiceProviderActivator(sp));
                    })
                    .AddHangfireServer();
                })
                .Configure(app =>
                {
                    app.UseHangfireDashboard();
                });
            })
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureServices((context, services) =>
            {
                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers
                services.AddTransient<IActivationHandler, AppNotificationActivationHandler>();
                services.AddTransient<IActivationHandler, AppProtocolActivationHandler>();
                services.AddTransient<IActivationHandler, AppProtocolFromLaunchActivationHandler>();

                // Services
                services.AddSingleton<IAppNotificationService, AppNotificationService>();
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddTransient<DownloadInspectionHandler>();
                services.AddTransient<Project>();
                services.AddSingleton<IDocumentFactory, SyncfusionDocumentFactory>();
                services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

                // Core Services
                services.AddSingleton<IDownloadService, DownloadService>();
                services.AddSingleton<IContainerExtension, Prism.DryIoc.DryIocContainerExtension>();
                services.AddSingleton<IFileService, FileService>();
                services.AddSingleton<IPackFactory, PackFactory>();
                services.AddSingleton<ITemplateRegistry, TemplateRegistry>(sp =>
                {
                    var tr = new TemplateRegistry(
                        sp.GetRequiredService<IPackFactory>(),
                        sp.GetService<IOptions<SettingsDirectoryPaths>>());
                    tr.LoadMachineTemplates();
                    tr.LoadUserTemplates();
                    return tr;
                });
                services.AddSingleton<ILogicRegistry, LogicRegistry>();
                services.AddSingleton<IInspectionFactory, InspectionFactory>(sp =>
                {
                    return new InspectionFactory(
                        sp,
                        templateRegistry: sp.GetService<ITemplateRegistry>(),
                        packFactory: sp.GetService<IPackFactory>(),
                        logicRegistry: sp.GetService<ILogicRegistry>());
                });
                services.AddTransient<IJsonDeserializerV2, JsonDeserializerV2>();
                services.AddTransient<IJsonSerializerV2, JsonSerializerV2>();
                services.AddTransient<PT.Inspection.Wpf.Inspection>(sp =>
                {
                    var t = typeof(PT.Inspection.Wpf.Inspection);
                    var constructors = t.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
                    var constructor = constructors.FirstOrDefault();
                    if (constructor is null)
                    {
#pragma warning disable CS8603 // Possible null reference return.
                        return null;
#pragma warning restore CS8603 // Possible null reference return.
                    }

                    var @params = new object[constructor.GetParameters().Length];
                    var instance = constructor.Invoke(@params);
                    return (PT.Inspection.Wpf.Inspection)instance;
                });
                services.AddSingleton<IReportRegistry, ReportRegistry>();

                // Views and ViewModels
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<MainPage>();
                services.AddTransient<ShellPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<DownloadsViewModel>();
                services.AddTransient<DownloadsPage>();

                // TODO: New pages that will replace the ones above
                services.AddTransient<SettingsUpdatedViewModel>();
                services.AddTransient<SettingsUpdatedPage>();
                services.AddTransient<DownloadsListViewModel>();
                services.AddTransient<DownloadsListPage>();
                services.AddTransient<DownloadDetailViewModel>();
                services.AddTransient<DownloadDetailPage>();

                services.AddHttpClient<IHubService, HubService>()
                    .ConfigureHttpMessageHandlerBuilder(builder =>
                    {
                        builder.PrimaryHandler = new HttpClientHandler()
                        {
                            DefaultProxyCredentials = CredentialCache.DefaultCredentials,
                            Proxy = WebRequest.GetSystemWebProxy(),
                            UseDefaultCredentials = true,
                        };
                    });

                services.Configure<LocalSettingsOptions>(
                    context.Configuration.GetSection(nameof(LocalSettingsOptions)));
                services.Configure<DownloadSettingsOptions>(
                    context.Configuration.GetSection(nameof(DownloadSettingsOptions)));
                services.AddOptions<SettingsDirectoryPaths>()
                .Configure(directories =>
                {
                    if (directories is null)
                    {
                        return;
                    }

                    directories.FamilyMachineSettingsDirectory =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                            @"PipeTech");

                    if (!string.IsNullOrEmpty(directories?.FamilyMachineSettingsDirectory))
                    {
                        var di = new DirectoryInfo(directories.FamilyMachineSettingsDirectory);

                        try
                        {
                            if (!di.Exists)
                            {
                                di = Directory.CreateDirectory(directories.FamilyMachineSettingsDirectory);
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            if (di.Exists)
                            {
                                di.EnsureEveryonePermissions();
                            }
                        }
                        catch
                        {
                        }
                    }

                    directories!.MachineSettingsDirectory =
                        Path.Combine(
                            directories.FamilyMachineSettingsDirectory,
                            Assembly.GetEntryAssembly()?.GetName().Name ?? "PipeTech.Downloader");

                    if (!string.IsNullOrEmpty(directories?.MachineSettingsDirectory))
                    {
                        var di = new DirectoryInfo(directories.MachineSettingsDirectory);

                        try
                        {
                            if (!di.Exists)
                            {
                                di = Directory.CreateDirectory(directories.MachineSettingsDirectory);
                            }
                        }
                        catch
                        {
                        }

                        try
                        {
                            if (di.Exists)
                            {
                                di.EnsureEveryonePermissions();
                            }
                        }
                        catch
                        {
                        }
                    }

                    directories!.FamilyUserSettingsDirectory =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            @"PipeTech");

                    directories.UserSettingsDirectory =
                        Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            Assembly.GetEntryAssembly()?.GetName().Name ?? "PipeTech.Downloader");
                });
            })
            .UseSerilog(
            (context, s, config) =>
            {
                var opt = s.GetService<IOptions<SettingsDirectoryPaths>>();
                var cool = opt?.Value;

                config?
                ////.ReadFrom.Configuration(context.Configuration, "Logging")
                .ReadFrom.Configuration(context.Configuration, new ConfigurationReaderOptions() { SectionName = "Logging" })
                .ReadFrom.Services(s)
                .Enrich.FromLogContext()
                 .WriteTo.File(
                         Path.Combine(
                             cool?.MachineSettingsDirectory ?? string.Empty,
                             $"PipeTech Downloader.log"),
                         rollingInterval: RollingInterval.Day,
                         buffered: true,
                         flushToDiskInterval: TimeSpan.FromSeconds(1),
                         outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}");
            },
            writeToProviders: true)
            .Build();

        App.GetService<IAppNotificationService>().Initialize();
        _ = Task.Run(() =>
        {
            try
            {
                using var db = new SQLite.SQLiteConnection("pipetech_downloader_jobs.db");
                _ = db.Execute("VACUUM;");
            }
            catch
            {
            }

            App.GetService<ITemplateRegistry>();

            var rr = App.GetService<IReportRegistry>();
            var container = App.GetService<IContainerExtension>();
            container?.RegisterSingleton(typeof(IReportRegistry), () => rr);

            var drm = new DefaultReportModule();
            drm.RegisterTypes(container);
            drm.OnInitialized(container);

            var sdrm = new PT.Inspection.Reporting.Sewer.DefaultSewerReportModule();
            sdrm.RegisterTypes(container);
            sdrm.OnInitialized(container);

            var scdrm = new PT.Inspection.Reporting.Sewer.Custom.CustomSewerReportingModule();
            scdrm.RegisterTypes(container);
            scdrm.OnInitialized(container);

            var sndrm = new PT.Inspection.Reporting.Sewer.NASSCO.NASSCOReportModule();
            sndrm.RegisterTypes(container);
            sndrm.OnInitialized(container);

            var swdrm = new PT.Inspection.Reporting.Sewer.WSA.WSAReportModule();
            swdrm.RegisterTypes(container);
            swdrm.OnInitialized(container);

            var logicRegistry = App.GetService<ILogicRegistry>();
            container?.Register<ModelLogic>();
            logicRegistry?.Register<WSALogic>("Model.Sewer.WSA2020");
            logicRegistry?.Register<NASSCOv6Logic>("Model.Sewer.NASSCOv6");
            logicRegistry?.Register<NASSCOv7Logic>("Model.Sewer.NASSCOv7");

            SyncfusionLicenseProvider.RegisterLicense(Constants.SyncfusionLicenseCode);

            try
            {
                App.GetService<IDownloadService>();
            }
            catch (Exception)
            {
                throw;
            }
        });

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

        _ = this.Host.RunAsync();

        ////App.GetService<IAppNotificationService>().Show(string.Format("AppNotificationSamplePayload".GetLocalized(), AppContext.BaseDirectory));

        await App.GetService<IActivationService>().ActivateAsync(args);
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        try
        {
            App.GetService<ILogger<Program>>()?.LogError(e.Exception, "Unhandled error");
        }
        catch (Exception)
        {
        }
    }

    private async void OnActivated(object? sender, AppActivationArguments args)
    {
        if (args.Kind == ExtendedActivationKind.Protocol &&
            args.Data is Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs p)
        {
            await App.GetService<IActivationService>().ActivateAsync(p);
        }
        else
        {
            await App.GetService<IActivationService>().ActivateAsync(args);
        }
    }
}
