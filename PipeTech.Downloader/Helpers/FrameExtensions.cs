// <copyright file="FrameExtensions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>
using Microsoft.UI.Xaml.Controls;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Frame extension class.
/// </summary>
public static class FrameExtensions
{
    /// <summary>
    /// Get page view model from frame.
    /// </summary>
    /// <param name="frame">Frame.</param>
    /// <returns>View model.</returns>
    public static object? GetPageViewModel(this Frame frame) => frame?.Content?.GetType().GetProperty("ViewModel")?.GetValue(frame.Content, null);
}
