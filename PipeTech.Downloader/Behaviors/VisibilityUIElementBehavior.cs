// <copyright file="VisibilityUIElementBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Runtime.CompilerServices;
using Microsoft.UI.Xaml;
using Microsoft.Xaml.Interactivity;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Visible ui element behavior.
/// </summary>
public class VisibilityUIElementBehavior : Behavior<UIElement>
{
    private Models.DownloadInspection.States state;

    /// <summary>
    /// Gets or sets a value indicating whether to be only visible on Errored state.
    /// </summary>
    public bool OnlyVisibleOnError
    {
        get; set;
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

    private void OnStateChanged()
    {
        if (this.AssociatedObject is not null)
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
