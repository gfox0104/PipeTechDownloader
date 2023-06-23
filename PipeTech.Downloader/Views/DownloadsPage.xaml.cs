// <copyright file="DownloadsPage.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;

using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

/// <summary>
/// Downloads page.
/// </summary>
public sealed partial class DownloadsPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadsPage"/> class.
    /// </summary>
    public DownloadsPage()
    {
        this.ViewModel = App.GetService<DownloadsViewModel>();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public DownloadsViewModel ViewModel
    {
        get;
    }
}
