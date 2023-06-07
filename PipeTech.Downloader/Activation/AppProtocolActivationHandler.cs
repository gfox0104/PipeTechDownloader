// <copyright file="AppProtocolActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Protocol Activation handler class.
/// </summary>
public class AppProtocolActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
{
    /// <inheritdoc/>
    protected override Task HandleInternalAsync(LaunchActivatedEventArgs args)
    {
        return Task.CompletedTask;
    }
}
