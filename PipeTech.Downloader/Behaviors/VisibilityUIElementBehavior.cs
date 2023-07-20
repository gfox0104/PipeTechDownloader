// <copyright file="VisibilityUIElementBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Windows.Data;
using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Visible ui element behavior.
/// </summary>
public class VisibilityUIElementBehavior : Behavior<UIElement>
{
    /// <summary>
    /// Dependency property for DataFolder.
    /// </summary>
    public static readonly Microsoft.UI.Xaml.DependencyProperty DataFolderProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(DataFolder),
            typeof(string),
            typeof(VisibilityUIElementBehavior),
            new Microsoft.UI.Xaml.PropertyMetadata(null, DataFolderChanged));

    private Models.DownloadInspection.States state;
    private Models.DownloadInspectionHandler? downloadHandler;

    /// <summary>
    /// Gets or sets a value indicating whether to be only visible on Errored state.
    /// </summary>
    public bool OnlyVisibleOnError
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to be only visible by existence.
    /// </summary>
    public bool VisibleByExistence
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the download handler.
    /// </summary>
    public DownloadInspectionHandler? DownloadHandler
    {
        get => this.downloadHandler;
        set
        {
            if (this.downloadHandler != value)
            {
                if (this.downloadHandler is not null)
                {
                    this.downloadHandler.PropertyChanging -= this.DownloadHandler_PropertyChanging;
                    this.downloadHandler.PropertyChanged -= this.DownloadHandler_PropertyChanged;

                    if (this.downloadHandler.Inspection is not null)
                    {
                        this.downloadHandler.Inspection.PropertyChanged -= this.Inspection_PropertyChanged;
                    }
                }

                this.downloadHandler = value;

                if (this.downloadHandler is not null)
                {
                    this.downloadHandler.PropertyChanging += this.DownloadHandler_PropertyChanging;
                    this.downloadHandler.PropertyChanged += this.DownloadHandler_PropertyChanged;
                    if (this.downloadHandler.Inspection is not null)
                    {
                        this.downloadHandler.Inspection.PropertyChanged += this.Inspection_PropertyChanged;
                    }
                }

                this.OnStateChanged();
            }
        }
    }

    /// <summary>
    /// Gets or sets the data folder.
    /// </summary>
    public string DataFolder
    {
        get => (string)this.GetValue(DataFolderProperty);
        set => this.SetValue(DataFolderProperty, value);
    }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public Models.DownloadInspection.States State
    {
        get => this.state;
        set
        {
            if (this.state != value)
            {
                this.state = value;
                this.OnStateChanged();
            }
        }
    }

    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();
        this.OnStateChanged();
    }

    /// <inheritdoc/>
    protected override void OnDetaching() => base.OnDetaching();

    /// <summary>
    /// State changed.
    /// </summary>
    protected void OnStateChanged()
    {
        if (this.AssociatedObject is not null)
        {
            if (this.VisibleByExistence)
            {
                if (string.IsNullOrEmpty(this.DataFolder) ||
                    this.DownloadHandler is null ||
                    this.DownloadHandler.Inspection is null)
                {
                    this.AssociatedObject.Visibility = Visibility.Collapsed;
                }
                else
                {
                    var dir = Path.Combine(
                        this.DataFolder!,
                        this.DownloadHandler.Inspection.Name ?? "Inspection");
                    this.AssociatedObject.Visibility = Directory.Exists(dir) ?
                        Visibility.Visible :
                        Visibility.Collapsed;
                }
            }
            else
            {
                if (this.State == Models.DownloadInspection.States.Staged)
                {
                    this.AssociatedObject.Visibility = Visibility.Collapsed;
                }
                else
                {
                    if (!this.OnlyVisibleOnError ||
                        this.State == Models.DownloadInspection.States.Errored)
                    {
                        this.AssociatedObject.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        this.AssociatedObject.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
    }

    private static void DataFolderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is VisibilityUIElementBehavior vb)
        {
            vb.OnStateChanged();
        }
    }

    private void DownloadHandler_PropertyChanging(object? sender, System.ComponentModel.PropertyChangingEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (this.DownloadHandler?.Inspection is not null)
                {
                    this.DownloadHandler.Inspection.PropertyChanged -= this.Inspection_PropertyChanged;
                }

                break;
        }
    }

    private void DownloadHandler_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (this.DownloadHandler?.Inspection is not null)
                {
                    this.DownloadHandler.Inspection.PropertyChanged += this.Inspection_PropertyChanged;
                }

                break;
        }
    }

    private void Inspection_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspection.Name):
                this.OnStateChanged();
                break;
        }
    }
}
