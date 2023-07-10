// <copyright file="IDownloadService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Hangfire.Common;
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

    /// <summary>
    /// Find Hangfire jobs for the project.
    /// </summary>
    /// <param name="project">Project.</param>
    /// <returns>Dictionary of the job ids and Job themselves.</returns>
    public IEnumerable<KeyValuePair<string, Job>> FindJobForProject(Project project);

    /// <summary>
    /// Create job for a project.
    /// </summary>
    /// <param name="project">Project.</param>
    /// <returns>Asynchronous task.</returns>
    public Task CreateJobForProject(Project project);

#if DEBUG
    /// <summary>
    /// Test method.
    /// </summary>
    /// <param name="token">Cancellation token.</param>
    /// <returns>Asynchronous task.</returns>
    public Task Test(CancellationToken token);
#endif
}
