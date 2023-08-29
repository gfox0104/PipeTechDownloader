// <copyright file="SettingsUpdatedViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Settings updated page view model class.
/// </summary>
public class SettingsUpdatedViewModel : BindableRecipient, INavigationAware
{
    /// <inheritdoc/>
    public void OnNavigatedFrom()
    {
    }

    /// <inheritdoc/>
    public void OnNavigatedTo(object parameter)
    {
    }
}
