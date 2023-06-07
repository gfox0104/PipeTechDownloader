// <copyright file="IPageService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Page service interface.
/// </summary>
public interface IPageService
{
    /// <summary>
    /// Get the page type.
    /// </summary>
    /// <param name="key">Key of the page.</param>
    /// <returns>Type of the page.</returns>
    /// <exception cref="ArgumentException">Page not found.</exception>
    Type GetPageType(string key);
}
