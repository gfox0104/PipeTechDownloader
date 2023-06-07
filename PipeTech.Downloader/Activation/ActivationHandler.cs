// <copyright file="ActivationHandler.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Activation;

/// <summary>
///  Extend this class to implement new ActivationHandlers. See DefaultActivationHandler for an example.
/// </summary>
/// <typeparam name="T">Type.</typeparam>
/// <remarks>https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/activation.md .</remarks>
public abstract class ActivationHandler<T> : IActivationHandler
    where T : class
{
    /// <inheritdoc/>
    public bool CanHandle(object args) => args is T && this.CanHandleInternal((args as T)!);

    /// <inheritdoc/>
    public async Task HandleAsync(object args) => await this.HandleInternalAsync((args as T)!);

    /// <summary>
    /// Override this method to add the logic for whether to handle the activation.
    /// </summary>
    /// <param name="args">Arguments.</param>
    /// <returns>An indicator of whether to handle activation.</returns>
    protected virtual bool CanHandleInternal(T args) => true;

    /// <summary>
    /// Override this method to add the logic for your activation handler.
    /// </summary>
    /// <param name="args">Arguments.</param>
    /// <returns>Asynchronous Task.</returns>
    protected abstract Task HandleInternalAsync(T args);
}
