// <copyright file="IActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Activation;

/// <summary>
/// Activation handler interface.
/// </summary>
public interface IActivationHandler
{
    /// <summary>
    /// Indicates whether the handler can handle the activation per the arguments.
    /// </summary>
    /// <param name="args">Arguments.</param>
    /// <returns>Can handle or not.</returns>
    bool CanHandle(object args);

    /// <summary>
    /// Handler's handle method.
    /// </summary>
    /// <param name="args">Arguments.</param>
    /// <returns>Asynchronous task.</returns>
    Task HandleAsync(object args);
}
