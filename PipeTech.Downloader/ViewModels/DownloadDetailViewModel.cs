// <copyright file="DownloadDetailViewModel.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Contracts.ViewModels;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.ViewModels;

/// <summary>
/// Download Detail page view model class.
/// </summary>
public class DownloadDetailViewModel : BindableRecipient, INavigationAware
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
