// <copyright file="DownloadDetailPage.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

/// <summary>
/// Download detail page.
/// </summary>
public sealed partial class DownloadDetailPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadDetailPage"/> class.
    /// </summary>
    public DownloadDetailPage()
    {
        this.ViewModel = App.GetService<DownloadDetailViewModel>();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public DownloadDetailViewModel ViewModel
    {
        get;
    }
}
