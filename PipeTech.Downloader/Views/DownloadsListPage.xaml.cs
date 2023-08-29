// <copyright file="DownloadsListPage.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

/// <summary>
/// Downloads list page.
/// </summary>
public sealed partial class DownloadsListPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadsListPage"/> class.
    /// </summary>
    public DownloadsListPage()
    {
        this.ViewModel = App.GetService<DownloadsListViewModel>();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public DownloadsListViewModel ViewModel
    {
        get;
    }
}
