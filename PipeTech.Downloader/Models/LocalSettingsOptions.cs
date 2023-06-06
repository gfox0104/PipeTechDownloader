// <copyright file="LocalSettingsOptions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Models;

/// <summary>
/// Local setting options.
/// </summary>
public class LocalSettingsOptions
{
    /// <summary>
    /// Gets or sets the application data folder.
    /// </summary>
    public string? ApplicationDataFolder
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the local settings file.
    /// </summary>
    public string? LocalSettingsFile
    {
        get; set;
    }
}
