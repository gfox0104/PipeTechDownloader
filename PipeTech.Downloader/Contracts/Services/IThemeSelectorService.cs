// <copyright file="IThemeSelectorService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Theme selector service.
/// </summary>
public interface IThemeSelectorService
{
    /// <summary>
    /// Gets the theme.
    /// </summary>
    ElementTheme Theme
    {
        get;
    }

    /// <summary>
    /// Initialize the theme.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    Task InitializeAsync();

    /// <summary>
    /// Set the theme.
    /// </summary>
    /// <param name="theme">Theme to set.</param>
    /// <returns>Asynchronous task.</returns>
    Task SetThemeAsync(ElementTheme theme);

    /// <summary>
    /// Sets the requested theme.
    /// </summary>
    /// <returns>Asynchronous task.</returns>
    Task SetRequestedThemeAsync();
}
