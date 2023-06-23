// <copyright file="IActivationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Activation service interface.
/// </summary>
public interface IActivationService
{
    /// <summary>
    /// Activate.
    /// </summary>
    /// <param name="activationArgs">Activation arguments.</param>
    /// <returns>Asynchronous task.</returns>
    Task ActivateAsync(object activationArgs);
}
