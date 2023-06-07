// <copyright file="DefaultActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

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

    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultActivationHandler"/> class.
    /// </summary>
    /// <param name="navigationService">Navigation service.</param>
    public DefaultActivationHandler(INavigationService navigationService)
    {
        this.navigationService = navigationService;
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
        this.navigationService.NavigateTo(typeof(DownloadsViewModel).FullName!, args.Arguments);
        await Task.CompletedTask;
    }
}
