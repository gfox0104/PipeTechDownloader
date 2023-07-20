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
    /// Show a notification that closes after a delay.
    /// </summary>
    /// <param name="payload">Payload.</param>
    /// <param name="delay">Delay.</param>
    /// <returns>An indicator whether notification is shown.</returns>
    bool Show(string payload, TimeSpan delay);

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
