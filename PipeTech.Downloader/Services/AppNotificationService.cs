// <copyright file="AppNotificationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.Specialized;
using System.Web;
using Microsoft.Windows.AppNotifications;
using PipeTech.Downloader.Contracts.Services;

namespace PipeTech.Downloader.Notifications;

/// <summary>
/// App notification service class.
/// </summary>
public class AppNotificationService : IAppNotificationService
{
    private readonly INavigationService navigationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppNotificationService"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public AppNotificationService(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="AppNotificationService"/> class.
    /// </summary>
    ~AppNotificationService()
    {
        this.Unregister();
    }

    /// <inheritdoc/>
    public void Initialize()
    {
        AppNotificationManager.Default.NotificationInvoked += this.OnNotificationInvoked;

        AppNotificationManager.Default.Register();
    }

    /// <summary>
    /// Notification is invoke.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="args">Arguments.</param>
    public void OnNotificationInvoked(AppNotificationManager sender, AppNotificationActivatedEventArgs args)
    {
        // TODO: Handle notification invocations when your app is already running, if REQUIRED (Currently not)

        //// // Navigate to a specific page based on the notification arguments.
        //// if (ParseArguments(args.Argument)["action"] == "Settings")
        //// {
        ////    App.MainWindow.DispatcherQueue.TryEnqueue(() =>
        ////    {
        ////        _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        ////    });
        //// }

        ////App.MainWindow.DispatcherQueue.TryEnqueue(() =>
        ////{
        ////    App.MainWindow.ShowMessageDialogAsync(
        ////        "TODO: Handle notification invocations when your app is already running.",
        ////        "Notification Invoked");

        ////    App.MainWindow.BringToFront();
        ////});
    }

    /// <inheritdoc/>
    public bool Show(string payload)
    {
        var appNotification = new AppNotification(payload);

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }

    /// <inheritdoc/>
    public bool Show(string payload, TimeSpan delay)
    {
        var appNotification = new AppNotification(payload);
        appNotification.Expiration = DateTime.Now + delay;

        AppNotificationManager.Default.Show(appNotification);

        return appNotification.Id != 0;
    }

    /// <inheritdoc/>
    public NameValueCollection ParseArguments(string arguments)
    {
        return HttpUtility.ParseQueryString(arguments);
    }

    /// <inheritdoc/>
    public void Unregister()
    {
        AppNotificationManager.Default.Unregister();
    }
}
