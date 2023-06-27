// <copyright file="BindingProxy.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Windows;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Provides a binding proxy for xaml.
/// </summary>
public class BindingProxy : Freezable
{
    /// <summary>
    /// Dependency property for <see cref="Data"/>.
    /// </summary>
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(
            nameof(Data),
            typeof(object),
            typeof(BindingProxy),
            new FrameworkPropertyMetadata(null));

    /// <summary>
    /// Gets or sets the data value.
    /// </summary>
    public object Data
    {
        get => this.GetValue(DataProperty);
        set => this.SetValue(DataProperty, value);
    }

    /// <inheritdoc/>
    protected override Freezable CreateInstanceCore() => new BindingProxy();
}
