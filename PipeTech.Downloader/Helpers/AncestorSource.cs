// <copyright file="AncestorSource.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Routing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Ancestor Source class.
/// </summary>
public static class AncestorSource
{
    /// <summary>
    /// Ancestor type property.
    /// </summary>
    public static readonly DependencyProperty AncestorTypeProperty =
        DependencyProperty.RegisterAttached(
            "AncestorType",
            typeof(Type),
            typeof(AncestorSource),
            new PropertyMetadata(default(Type), OnAncestorTypeChanged));

    /// <summary>
    /// Set of the ancestor type.
    /// </summary>
    /// <param name="element">Element.</param>
    /// <param name="value">Value.</param>
    public static void SetAncestorType(FrameworkElement element, Type value) =>
        element.SetValue(AncestorTypeProperty, value);

    /// <summary>
    /// Get the ancestor type.
    /// </summary>
    /// <param name="element">Element.</param>
    /// <returns>Value.</returns>
    public static Type GetAncestorType(FrameworkElement element) =>
        (Type)element.GetValue(AncestorTypeProperty);

    private static void OnAncestorTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        FrameworkElement target = (FrameworkElement)d;
        if (target.IsLoaded)
        {
            SetDataContext(target);
        }
        else
        {
            target.Loaded += OnTargetLoaded;
        }
    }

    private static void OnTargetLoaded(object sender, RoutedEventArgs e)
    {
        FrameworkElement target = (FrameworkElement)sender;
        target.Loaded -= OnTargetLoaded;
        SetDataContext(target);
    }

    private static void SetDataContext(FrameworkElement target)
    {
        var ancestorType = GetAncestorType(target);
        if (ancestorType is not null)
        {
            target.DataContext = FindParentDataContext(target, ancestorType);
        }
    }

    private static object? FindParentDataContext(DependencyObject dependencyObject, Type ancestorType)
    {
        DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);
        if (parent == null)
        {
            return null;
        }

        if (parent is FrameworkElement fe &&
            fe.DataContext is not null &&
            ancestorType.IsAssignableFrom(fe.DataContext.GetType()))
        {
            return fe.DataContext;
        }

        return FindParentDataContext(parent, ancestorType);
    }
}