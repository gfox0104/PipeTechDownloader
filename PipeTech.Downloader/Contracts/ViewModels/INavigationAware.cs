// <copyright file="INavigationAware.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Contracts.ViewModels;

/// <summary>
/// Navigation aware interface.
/// </summary>
public interface INavigationAware
{
    /// <summary>
    /// View has been navigated to.
    /// </summary>
    /// <param name="parameter">Navigation parameters.</param>
    void OnNavigatedTo(object parameter);

    /// <summary>
    /// View has been navigated from.
    /// </summary>
    void OnNavigatedFrom();
}
