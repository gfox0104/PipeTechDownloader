// <copyright file="IDownloadService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Interface for the download service.
/// </summary>
public interface IDownloadService
{
    /// <summary>
    /// Gets the source project data.
    /// </summary>
    public ObservableCollection<Project> Source
    {
        get;
    }

    /// <summary>
    /// Load the download from the file system.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Asynchronous task.</returns>
    public Task LoadDownloads(CancellationToken token = default);

    /// <summary>
    /// Download the project.
    /// </summary>
    /// <param name="projectFilePath">Project file path.</param>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Asynchronous task.</returns>
    public Task DownloadProject(string projectFilePath, CancellationToken token);

    /////// <summary>
    /////// Download an inspection.
    /////// </summary>
    /////// <param name="projectFilePath">Project file path.</param>
    /////// <param name="inspectionPath">Inspection path.</param>
    /////// <param name="token">Cancellation token.</param>
    /////// <returns>Asynchronous task.</returns>
    ////public Task DownloadInspection(string projectFilePath, string inspectionPath, CancellationToken token);

#if DEBUG
    /// <summary>
    /// Test method.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Asynchronous task.</returns>
    public Task Test(CancellationToken token);
#endif
}
