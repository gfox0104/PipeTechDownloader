// <copyright file="AppProtocolFromLaunchActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CacheManager.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Protocol Activation handler class.
/// </summary>
public class AppProtocolFromLaunchActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService navigationService;
    private readonly ILogger<AppProtocolFromLaunchActivationHandler>? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppProtocolFromLaunchActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger service.</param>
    public AppProtocolFromLaunchActivationHandler(
        INavigationService navigationService,
        ILogger<AppProtocolFromLaunchActivationHandler>? logger = null)
    {
        this.navigationService = navigationService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        this.logger?.LogDebug($"{nameof(this.CanHandleInternal)}: {args.Arguments}");
        var launchArgs = AppInstance.GetCurrent().GetActivatedEventArgs();
        this.logger?.LogDebug($"{nameof(this.CanHandleInternal)} feat activated: {launchArgs.Kind} : {launchArgs.Data}");
        if (launchArgs.Kind == ExtendedActivationKind.Protocol &&
            launchArgs.Data is Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs pargs &&
            pargs.Uri is Uri)
        {
            return true;
        }

        return false;
    }

    /// <inheritdoc/>
    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        this.logger?.LogDebug($"{nameof(this.HandleInternalAsync)}: {args}");

        var launchArgs = AppInstance.GetCurrent().GetActivatedEventArgs();

        if (launchArgs.Kind != ExtendedActivationKind.Protocol ||
            launchArgs.Data is not Windows.ApplicationModel.Activation.ProtocolActivatedEventArgs pargs ||
            pargs.Uri is not Uri uri)
        {
            return;
        }

        // Queue navigation with low priority to allow the UI to initialize.
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            var m = this.navigationService.Frame?.GetPageViewModel() as MainViewModel;
            m?.CloseCommand?.Execute(null);
            this.navigationService.NavigateTo(typeof(MainViewModel).FullName!, parameter: uri);
            try
            {
                App.MainWindow.IsAlwaysOnTop = true;
            }
            catch (Exception)
            {
            }

            try
            {
                App.MainWindow.IsAlwaysOnTop = false;
            }
            catch (Exception)
            {
            }
        });

        await Task.CompletedTask;
    }
}
