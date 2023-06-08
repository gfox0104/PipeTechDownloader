// <copyright file="INavigationService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace PipeTech.Downloader.Contracts.Services;

/// <summary>
/// Navigation service interface.
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Navigated event.
    /// </summary>
    event NavigatedEventHandler Navigated;

    /// <summary>
    /// Gets a value indicating whether navigation can go back.
    /// </summary>
    bool CanGoBack
    {
        get;
    }

    /// <summary>
    /// Gets or sets the current frame.
    /// </summary>
    Frame? Frame
    {
        get; set;
    }

    /// <summary>
    /// Navigate to a page.
    /// </summary>
    /// <param name="pageKey">Key of the page to navigate to.</param>
    /// <param name="parameter">Parameters for navigation.</param>
    /// <param name="clearNavigation">Whether to clear the navigation history.</param>
    /// <returns>Success indicator.</returns>
    bool NavigateTo(string pageKey, object? parameter = null, bool clearNavigation = false);

    /// <summary>
    /// Navigate back in the journal.
    /// </summary>
    /// <returns>Success indicator.</returns>
    bool GoBack();
}
