// <copyright file="ButtonContentTemplateBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using PipeTech.Downloader.Models;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Button content template behavior class.
/// </summary>
public class ButtonContentTemplateBehavior : Behavior<Button>, INotifyPropertyChanged
{
    private DownloadInspection.States state;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the pause template.
    /// </summary>
    public DataTemplate? PauseTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the restart template.
    /// </summary>
    public DataTemplate? RestartTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the complete template.
    /// </summary>
    public DataTemplate? CompletedTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the errored template.
    /// </summary>
    public DataTemplate? ErroredTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public DownloadInspection.States State
    {
        get => this.state;
        set
        {
            if (this.state != value)
            {
                this.state = value;
                this.PropertyChanged?.Invoke(this, new(nameof(this.State)));
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

    private void OnStateChanged()
    {
        if (this.AssociatedObject is null)
        {
            return;
        }

        switch (this.State)
        {
            case DownloadInspection.States.Errored:
                this.AssociatedObject.ContentTemplate = this.ErroredTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            case DownloadInspection.States.Complete:
                this.AssociatedObject.ContentTemplate = this.CompletedTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            case DownloadInspection.States.Loading:
            case DownloadInspection.States.Queued:
            case DownloadInspection.States.Processing:
            case DownloadInspection.States.Staged:
                this.AssociatedObject.ContentTemplate = this.PauseTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            case DownloadInspection.States.Paused:
                this.AssociatedObject.ContentTemplate = this.RestartTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            default:
                break;
        }
    }
}
