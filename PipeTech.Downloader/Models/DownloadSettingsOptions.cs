// <copyright file="DownloadSettingsOptions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Models;

/// <summary>
/// Download settings from application settings.
/// </summary>
public class DownloadSettingsOptions
{
    /// <summary>
    /// Gets or sets the number of minutes in which to reschedule project downloads.
    /// </summary>
    public int RescheduleProjectDownloadInMinutes { get; set; } = 10;
}
