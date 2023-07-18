// <copyright file="AppProtocolActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CacheManager.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.ViewModels;
using Windows.ApplicationModel.Activation;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Protocol Activation handler class.
/// </summary>
public class AppProtocolActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
{
    private readonly INavigationService navigationService;
    private readonly ILogger<AppProtocolActivationHandler>? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppProtocolActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger service.</param>
    public AppProtocolActivationHandler(
        INavigationService navigationService,
        ILogger<AppProtocolActivationHandler>? logger = null)
    {
        this.navigationService = navigationService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    protected override bool CanHandleInternal(ProtocolActivatedEventArgs args)
    {
        this.logger?.LogDebug($"{nameof(this.CanHandleInternal)}: {args.Kind == ActivationKind.Protocol && args.Uri is Uri}");
        return args.Kind == ActivationKind.Protocol && args.Uri is Uri;
    }

    /// <inheritdoc/>
    protected async override Task HandleInternalAsync(ProtocolActivatedEventArgs args)
    {
        this.logger?.LogDebug($"{nameof(this.HandleInternalAsync)}: {args.Uri}");
        var uri = args.Uri;
        if (uri is null)
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
