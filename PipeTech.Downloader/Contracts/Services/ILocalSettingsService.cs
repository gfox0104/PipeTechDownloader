// <copyright file="ILocalSettingsService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Local settings interface.
/// </summary>
public interface ILocalSettingsService
{
    /// <summary>
    /// Key for the app background theme.
    /// </summary>
    internal const string AppBackgroundRequestedThemeKey = "AppBackgroundRequestedTheme";

    /// <summary>
    /// Key for the default data folder.
    /// </summary>
    internal const string LastDataFolderKey = "LastDataFolder";

    /// <summary>
    /// Key for the default data folder.
    /// </summary>
    internal const string CloseNotificationShownKey = "HasCloseNotificationBeenShown";

    /// <summary>
    /// Read the setting.
    /// </summary>
    /// <typeparam name="T">Type to read.</typeparam>
    /// <param name="key">Key to read from.</param>
    /// <returns>Object read.</returns>
    Task<T?> ReadSettingAsync<T>(string key);

    /// <summary>
    /// Save settings.
    /// </summary>
    /// <typeparam name="T">Type to save.</typeparam>
    /// <param name="key">Key.</param>
    /// <param name="value">Value to save.</param>
    /// <returns>Asynchronous task.</returns>
    Task SaveSettingAsync<T>(string key, T value);
}
