// <copyright file="SettingsUpdatedPage.xaml.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Views;

/// <summary>
/// Settings updated page.
/// </summary>
public sealed partial class SettingsUpdatedPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsUpdatedPage"/> class.
    /// </summary>
    public SettingsUpdatedPage()
    {
        this.ViewModel = App.GetService<SettingsUpdatedViewModel>();
        this.InitializeComponent();
    }

    /// <summary>
    /// Gets the view model.
    /// </summary>
    public SettingsUpdatedViewModel ViewModel
    {
        get;
    }
}
