// <copyright file="VisibilityButtonBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Visible button behavior.
/// </summary>
public class VisibilityButtonBehavior : Behavior<Button>
{
    private Models.DownloadInspection.States state;

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

                if (this.AssociatedObject is not null)
                {
                    if (this.state == Models.DownloadInspection.States.Staged)
                    {
                        this.AssociatedObject.Visibility = Microsoft.UI.Xaml.Visibility.Collapsed;
                    }
                    else
                    {
                        this.AssociatedObject.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
                    }
                }
            }
        }
    }

    /// <inheritdoc/>
    protected override void OnAttached()
    {
        base.OnAttached();
    }

    /// <inheritdoc/>
    protected override void OnDetaching() => base.OnDetaching();
}
