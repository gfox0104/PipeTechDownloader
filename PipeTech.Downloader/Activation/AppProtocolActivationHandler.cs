// <copyright file="AppProtocolActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.WinUI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.ViewModels;
using Windows.ApplicationModel.Activation;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Protocol Activation handler class.
/// </summary>
public class AppProtocolActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
{
    private readonly INavigationService navigationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppProtocolActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public AppProtocolActivationHandler(
        INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    /// <inheritdoc/>
    protected override bool CanHandleInternal(ProtocolActivatedEventArgs args)
    {
        return args.Kind == ActivationKind.Protocol && args.Uri is Uri;
    }

    /// <inheritdoc/>
    protected async override Task HandleInternalAsync(ProtocolActivatedEventArgs args)
    {
        var uri = args.Uri;
        if (uri is null)
        {
            return;
        }

        // Queue navigation with low priority to allow the UI to initialize.
        App.MainWindow.DispatcherQueue.TryEnqueue(DispatcherQueuePriority.Low, () =>
        {
            this.navigationService.NavigateTo(typeof(MainViewModel).FullName!, parameter: uri);
        });

        await Task.CompletedTask;
    }
}
