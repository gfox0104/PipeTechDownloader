// <copyright file="ActionButtonSelector.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Action button template selector.
/// </summary>
public class ActionButtonSelector : DataTemplateSelector
{
    /// <summary>
    /// Gets or sets the paused template.
    /// </summary>
    public DataTemplate? Paused
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the completed template.
    /// </summary>
    public DataTemplate? Completed
    {
        get; set;
    }

    /// <inheritdoc/>
    protected override DataTemplate SelectTemplateCore(object item)
    {
        return base.SelectTemplateCore(item);
    }
}
