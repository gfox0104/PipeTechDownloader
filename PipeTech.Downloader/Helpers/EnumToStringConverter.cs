// <copyright file="EnumToStringConverter.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Enum to string converter class.
/// </summary>
public class EnumToStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object parameter, string language)
    {
        if (value is Enum e)
        {
            return e.ToString();
        }

        return value?.ToString();
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
