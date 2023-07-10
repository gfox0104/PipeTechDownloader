// <copyright file="DefaultActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Default activation handler.
/// </summary>
public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    private readonly INavigationService navigationService;
    private readonly ILogger<DefaultActivationHandler>? logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    /// <param name="logger">Logger service.</param>
    public DefaultActivationHandler(
        INavigationService navigationService,
        ILogger<DefaultActivationHandler>? logger = null)
    {
        this.navigationService = navigationService;
        this.logger = logger;
    }

    /// <inheritdoc/>
    protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
    {
        // None of the ActivationHandlers has handled the activation.
        return this.navigationService.Frame?.Content == null;
    }

    /// <inheritdoc/>
    protected async override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        this.logger?.LogDebug($"Default activation handler. [{args.Arguments}]");

        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!, args.Arguments);
        });
        await Task.CompletedTask;
    }
}
