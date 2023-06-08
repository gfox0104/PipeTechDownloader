// <copyright file="DownloadInspection.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Download inspection class.
/// </summary>
public partial class DownloadInspection : ObservableRecipient
{
    [ObservableProperty]
    private long? size;

    [ObservableProperty]
    private ObservableCollection<string>? files;

    [ObservableProperty]
    private States state;

    [ObservableProperty]
    private long? totalSize;

    /// <summary>
    /// States of the download.
    /// </summary>
    public enum States
    {
        /// <summary>
        /// Loading.
        /// </summary>
        Loading,
    }
}
