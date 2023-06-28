// <copyright file="StringUriToFilenameConverter.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Converter class from a string that is a Uri to just the filename as a string.
/// </summary>
public class StringUriToFilenameConverter : IValueConverter
{
    /// <inheritdoc/>
    public object? Convert(object? value, Type targetType, object? parameter, string? language)
    {
        if (value is null)
        {
            return value;
        }

        if (!Uri.TryCreate(value.ToString(), default(UriCreationOptions), out var uri) || uri is null)
        {
            return value;
        }

        if (uri.IsAbsoluteUri)
        {
            return Path.GetFileName(Uri.UnescapeDataString(uri.AbsolutePath));
        }
        else if (uri.IsFile)
        {
        }
        else
        {
        }

        return value;
    }

    /// <inheritdoc/>
    public object? ConvertBack(object? value, Type targetType, object? parameter, string? language)
    {
        throw new NotImplementedException();
    }
}
