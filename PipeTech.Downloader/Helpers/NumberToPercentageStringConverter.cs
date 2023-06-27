// <copyright file="NumberToPercentageStringConverter.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Enum to boolean converter class.
/// </summary>
public class NumberToPercentageStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object parameter, string language)
    {
        if (value == null)
        {
            return null;
        }

        if (!decimal.TryParse(value.ToString(), out var d))
        {
            return null;
        }

        return $"{d * 100m:0.0}%";
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
