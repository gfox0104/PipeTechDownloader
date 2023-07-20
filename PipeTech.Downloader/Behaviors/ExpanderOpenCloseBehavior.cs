// <copyright file="ExpanderOpenCloseBehavior.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.ComponentModel;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace PipeTech.Downloader.Behaviors;

/// <summary>
/// Expander behavior.
/// </summary>
public class ExpanderOpenCloseBehavior : Behavior<CommunityToolkit.WinUI.UI.Controls.Expander>
{
    /// <summary>
    /// Dependency property for IsEnabled.
    /// </summary>
    public static readonly Microsoft.UI.Xaml.DependencyProperty CollectionViewProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(CollectionView),
            typeof(ICollectionView),
            typeof(ExpanderOpenCloseBehavior),
            new Microsoft.UI.Xaml.PropertyMetadata(null));

    /// <summary>
    /// Dependency property for VisibilityMode.
    /// </summary>
    public static readonly Microsoft.UI.Xaml.DependencyProperty VisibilityModeProperty =
        Microsoft.UI.Xaml.DependencyProperty.Register(
            nameof(VisibilityMode),
            typeof(DataGridRowDetailsVisibilityMode),
            typeof(ExpanderOpenCloseBehavior),
            new Microsoft.UI.Xaml.PropertyMetadata(DataGridRowDetailsVisibilityMode.Collapsed));

    private bool expanded;

    /// <summary>
    /// Gets or sets the state.
    /// </summary>
    ////public CommunityToolkit.WinUI.UI.Controls.DataGrid? DataGrid
    public ICollectionView CollectionView
    {
        get => (ICollectionView)this.GetValue(CollectionViewProperty);
        set => this.SetValue(CollectionViewProperty, value);
    }

    /// <summary>
    /// Gets or sets the row visibility.
    /// </summary>
    public DataGridRowDetailsVisibilityMode VisibilityMode
    {
        get => (DataGridRowDetailsVisibilityMode)this.GetValue(VisibilityModeProperty);
        set => this.SetValue(VisibilityModeProperty, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the expanded-ness of the expander.
    /// </summary>
    public bool Expanded
    {
        get => this.expanded;
        set
         {
            if (this.expanded != value)
            {
                this.expanded = value;

                this.VisibilityMode = this.expanded ?
                    DataGridRowDetailsVisibilityMode.VisibleWhenSelected :
                    DataGridRowDetailsVisibilityMode.Collapsed;

                if (!this.expanded && this.CollectionView is not null)
                {
                    this.CollectionView.MoveCurrentTo(null);
                }
            }
        }
    }
}
