// <copyright file="IAppNotificationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.Specialized;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Application notification service interface.
/// </summary>
public interface IAppNotificationService
{
    /// <summary>
    /// Initialize.
    /// </summary>
    void Initialize();

    /// <summary>
    /// Show a notification.
    /// </summary>
    /// <param name="payload">Payload.</param>
    /// <returns>An indicator whether notification is shown.</returns>
    bool Show(string payload);

    /// <summary>
    /// Parse arguments.
    /// </summary>
    /// <param name="arguments">Arguments to parse.</param>
    /// <returns>Parsed arguments.</returns>
    NameValueCollection ParseArguments(string arguments);

    /// <summary>
    /// Unregister.
    /// </summary>
    void Unregister();
}
