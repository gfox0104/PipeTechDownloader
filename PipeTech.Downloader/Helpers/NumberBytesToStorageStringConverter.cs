// <copyright file="NumberBytesToStorageStringConverter.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Number of byte to storage string converter class.
/// </summary>
public class NumberBytesToStorageStringConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object parameter, string language)
    {
        if (value == null)
        {
            return null;
        }

        if (!long.TryParse(value.ToString(), out var bytes))
        {
            return null;
        }

        if (bytes < Math.Pow(2, 20))
        {
            // 1024 * 1024
            // Return as kilo bytes
            return (bytes / Math.Pow(2, 10)).ToString("0.0 KB");
        }
        else if (bytes <= Math.Pow(2, 30) * 10)
        {
            // 1024 * 1024 * 1024
            // Return in MB
            return (bytes / Math.Pow(2, 20)).ToString("0.0 MB");
        }
        else
        {
            // Return in GB
            return (bytes / Math.Pow(2, 30)).ToString("0.0 GB");
        }
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
