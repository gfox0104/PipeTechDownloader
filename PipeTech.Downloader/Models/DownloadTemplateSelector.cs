// <copyright file="DownloadTemplateSelector.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Template data selector for download page.
/// </summary>
public class DownloadTemplateSelector : DataTemplateSelector
{
    /// <summary>
    /// Gets or sets the folder template.
    /// </summary>
    public DataTemplate? FolderTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the file template.
    /// </summary>
    public DataTemplate? FileTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the file template.
    /// </summary>
    public DataTemplate? FileTemplateString
    {
        get; set;
    }

    /// <inheritdoc/>
    protected override DataTemplate SelectTemplateCore(object item)
    {
        if (item is DownloadInspectionHandler)
        {
            return this.FolderTemplate ?? base.SelectTemplateCore(item);
        }
        else if (item is string)
        {
            return this.FileTemplateString ?? base.SelectTemplateCore(item);
        }

        return this.FileTemplate ?? base.SelectTemplateCore(item);
    }
}
