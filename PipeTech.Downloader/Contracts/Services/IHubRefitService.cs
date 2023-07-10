// <copyright file="IHubRefitService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Core.Models;
using Refit;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Hub refit interface.
/// </summary>
public interface IHubRefitService
{
    /// <summary>
    /// Get the manifest.
    /// </summary>
    /// <param name="id">Manifest id.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Manifest information.</returns>
    [Get("/manifest/{id}/")]
    public Task<ApiResponse<Manifest>> GetManifest(string id, CancellationToken token = default);

    /// <summary>
    /// Get the manifest link.
    /// </summary>
    /// <param name="id">Manifest id.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Manifest link.</returns>
    [Get("/api/downloadManifest/")]
    public Task<ApiResponse<string>> GetManifestLink(string id, CancellationToken token = default);
}
