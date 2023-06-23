// <copyright file="File.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using CommunityToolkit.Mvvm.ComponentModel;

namespace PipeTech.Downloader.Models;

/// <summary>
/// File class.
/// </summary>
public partial class File : BindableRecipient
{
    /// <summary>
    /// Gets or sets the file name. This many include the full path to uri.
    /// </summary>
    [ObservableProperty]
    private string name = string.Empty;

    /// <summary>
    /// Gets or sets the file's download path.
    /// </summary>
    [ObservableProperty]
    private string downloadPath = string.Empty;

    /// <summary>
    /// Gets or sets the downloaded size of the file.
    /// </summary>
    [ObservableProperty]
    private long? downloadedSize;

    /// <summary>
    /// Gets or sets the size of the file, if known.
    /// </summary>
    [ObservableProperty]
    private long? size;

    /// <summary>
    /// Gets or sets the progress.
    /// </summary>
    [ObservableProperty]
    private decimal progress = 0m;
}
