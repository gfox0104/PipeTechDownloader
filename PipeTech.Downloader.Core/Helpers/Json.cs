// <copyright file="Json.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Core.Helpers;

/// <summary>
/// Json helper class.
/// </summary>
public static class Json
{
    /// <summary>
    /// Json to object.
    /// </summary>
    /// <typeparam name="T">Type to make json.</typeparam>
    /// <param name="value">Json value.</param>
    /// <returns>Object.</returns>
    public static async Task<T?> ToObjectAsync<T>(string value)
    {
        return await Task.Run(() =>
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(value);
        });
    }

    /// <summary>
    /// Stringify object.
    /// </summary>
    /// <param name="value">Object to stringify.</param>
    /// <returns>Json string of the object.</returns>
    public static async Task<string> StringifyAsync(object? value)
    {
        return await Task.Run<string>(() =>
        {
            return System.Text.Json.JsonSerializer.Serialize(value);
        });
    }
}
