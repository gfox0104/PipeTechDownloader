// <copyright file="ResourceExtensions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.Windows.ApplicationModel.Resources;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Resource extensions.
/// </summary>
public static class ResourceExtensions
{
    private static readonly ResourceLoader ResourceLoader = new();

    /// <summary>
    /// Get localized string.
    /// </summary>
    /// <param name="resourceKey">Resource key for the string.</param>
    /// <returns>String requested.</returns>
    public static string GetLocalized(this string resourceKey) => ResourceLoader.GetString(resourceKey);
}
