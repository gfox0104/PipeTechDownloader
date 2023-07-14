// <copyright file="ContentPresenterTemplateManifestBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;
using PipeTech.Downloader.ViewModels;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Button content template behavior class.
/// </summary>
public class ContentPresenterTemplateManifestBehavior : Behavior<ContentPresenter>, INotifyPropertyChanged
{
    private MainViewModel.ManifestStates state;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets or sets the cancelled template.
    /// </summary>
    public DataTemplate? CancelledTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the loading template.
    /// </summary>
    public DataTemplate? LoadingTemplate
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
    /// Gets or sets the complete template.
    /// </summary>
    public DataTemplate? CompletedTemplate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    public MainViewModel.ManifestStates State
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
            case MainViewModel.ManifestStates.Cancelled:
                this.AssociatedObject.ContentTemplate = this.CancelledTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            case MainViewModel.ManifestStates.Loading:
                this.AssociatedObject.ContentTemplate = this.LoadingTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            case MainViewModel.ManifestStates.None:
                this.AssociatedObject.Visibility = Visibility.Collapsed;
                break;
            case MainViewModel.ManifestStates.Completed:
                this.AssociatedObject.ContentTemplate = this.CompletedTemplate;
                this.AssociatedObject.Visibility = Visibility.Collapsed;
                break;
            case MainViewModel.ManifestStates.Errored:
                this.AssociatedObject.ContentTemplate = this.ErroredTemplate;
                this.AssociatedObject.Visibility = Visibility.Visible;
                break;
            default:
                break;
        }
    }
}
