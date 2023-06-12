// <copyright file="IHubService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Core.Models;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Hub communication service interface.
/// </summary>
public interface IHubService
{
    /// <summary>
    /// Set the base address of the hub service.
    /// </summary>
    /// <param name="baseAddress">The base address to set to.</param>
    public void SetBaseAddress(Uri baseAddress);

    /// <summary>
    /// Get manifest information.
    /// </summary>
    /// <param name="id">ID for the manifest.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Manifest.</returns>
    Task<Manifest?> GetManifest(Guid id, CancellationToken token = default);
}
