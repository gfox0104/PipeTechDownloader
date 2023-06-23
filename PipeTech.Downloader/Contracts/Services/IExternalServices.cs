// <copyright file="IExternalServices.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Core.Models;
using Refit;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// External service refit interface.
/// </summary>
public interface IExternalServices
{
    /// <summary>
    /// Get the pack.
    /// </summary>
    /// <param name="packId">Pack id.</param>
    /// <param name="packVersion"> Version string of the pack requested.</param>
    /// <param name="descending">Boolean indicating whether to sort list of matched packs descending (True) or ascending (False).</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Manifest information.</returns>
    [Get("/pipetech/getpack?packId={packId}&packVersion={packVersion}&descending={descending}")]
    public Task<ApiResponse<Stream>> GetPack(
        Guid packId,
        string? packVersion = null,
        bool? descending = null,
        CancellationToken token = default);
}
