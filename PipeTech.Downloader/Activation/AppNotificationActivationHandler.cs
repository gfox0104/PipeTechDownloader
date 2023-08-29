// <copyright file="AppNotificationActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;

using PipeTech.Downloader.Contracts.Services;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// AppNotificationActivationHandler class.
/// </summary>
public class AppNotificationActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService navigationService;
    private readonly IAppNotificationService notificationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppNotificationActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="notificationService">Notification service.</param>
    public AppNotificationActivationHandler(
        INavigationService navigationService,
        IAppNotificationService notificationService)
    {
        this.navigationService = navigationService;
        this.notificationService = notificationService;
    }

    /// <inheritdoc/>
    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        return AppInstance.GetCurrent().GetActivatedEventArgs()?.Kind == ExtendedActivationKind.AppNotification;
    }

    /// <inheritdoc/>
    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        //// TODO: Handle notification activations, if REQUIRED (Currently not)

        //// // Access the AppNotificationActivatedEventArgs.
        //// var activatedEventArgs = (AppNotificationActivatedEventArgs)AppInstance.GetCurrent().GetActivatedEventArgs().Data;

        //// // Navigate to a specific page based on the notification arguments.
        //// if (_notificationService.ParseArguments(activatedEventArgs.Argument)["action"] == "Settings")
        //// {
        ////     // Queue navigation with low priority to allow the UI to initialize.
        ////     App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        ////     {
        ////         _navigationService.NavigateTo(typeof(SettingsViewModel).FullName!);
        ////     });
        //// }

        ////App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        ////{
        ////    App.MainWindow.ShowMessageDialogAsync("TODO: Handle notification activations.", "Notification Activation");
        ////});

        await Task.CompletedTask;
    }
}
