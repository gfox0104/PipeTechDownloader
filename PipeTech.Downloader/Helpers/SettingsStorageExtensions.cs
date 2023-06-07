// <copyright file="SettingsStorageExtensions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using PipeTech.Downloader.Core.Helpers;

using Windows.Storage;
using Windows.Storage.Streams;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Use these extension methods to store and retrieve local and roaming app data.
/// </summary>
/// <remarks>More details regarding storing and retrieving app data at https://docs.microsoft.com/windows/apps/design/app-settings/store-and-retrieve-app-data .</remarks>
public static class SettingsStorageExtensions
{
    private const string FileExtension = ".json";

    /// <summary>
    /// Determine if roaming storage is available.
    /// </summary>
    /// <param name="appData">Application data.</param>
    /// <returns>A value indicating whether roaming storage is available.</returns>
    public static bool IsRoamingStorageAvailable(this ApplicationData appData)
    {
        return appData.RoamingStorageQuota == 0;
    }

    /// <summary>
    /// Save data.
    /// </summary>
    /// <typeparam name="T">Type of content to save.</typeparam>
    /// <param name="folder">Folder path.</param>
    /// <param name="name">Name of file.</param>
    /// <param name="content">Content to save.</param>
    /// <returns>Asynchronous task.</returns>
    public static async Task SaveAsync<T>(this StorageFolder folder, string name, T content)
    {
        var file = await folder.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);
        var fileContent = string.Empty;
        if (content is not null)
        {
            await Json.StringifyAsync(content);
        }

        await FileIO.WriteTextAsync(file, fileContent ?? string.Empty);
    }

    /// <summary>
    /// Read an object from a file.
    /// </summary>
    /// <typeparam name="T">Type of the object to read.</typeparam>
    /// <param name="folder">Folder path.</param>
    /// <param name="name">File name.</param>
    /// <returns>Object read.</returns>
    public static async Task<T?> ReadAsync<T>(this StorageFolder folder, string name)
    {
        if (!File.Exists(Path.Combine(folder.Path, GetFileName(name))))
        {
            return default;
        }

        var file = await folder.GetFileAsync($"{name}.json");
        var fileContent = await FileIO.ReadTextAsync(file);

        return await Json.ToObjectAsync<T>(fileContent);
    }

    /// <summary>
    /// Save data as Json to settings.
    /// </summary>
    /// <typeparam name="T">Type of data to save.</typeparam>
    /// <param name="settings">Settings to save to.</param>
    /// <param name="key">Key.</param>
    /// <param name="value">Value.</param>
    /// <returns>Asynchronous task.</returns>
    public static async Task SaveAsync<T>(this ApplicationDataContainer settings, string key, T value)
    {
        settings.SaveString(key, await Json.StringifyAsync(value));
    }

    /// <summary>
    /// Save data as Json to settings.
    /// </summary>
    /// <param name="settings">Settings to save to.</param>
    /// <param name="key">Key.</param>
    /// <param name="value">Value.</param>
    public static void SaveString(this ApplicationDataContainer settings, string key, string value)
    {
        settings.Values[key] = value;
    }

    /// <summary>
    /// Read data from settings.
    /// </summary>
    /// <typeparam name="T">Type of object to read.</typeparam>
    /// <param name="settings">Settings to read from.</param>
    /// <param name="key">Key.</param>
    /// <returns>Object read.</returns>
    public static async Task<T?> ReadAsync<T>(this ApplicationDataContainer settings, string key)
    {
        object? obj;

        if (settings.Values.TryGetValue(key, out obj))
        {
            return await Json.ToObjectAsync<T>((string)obj);
        }

        return default;
    }

    /// <summary>
    /// Save byte array to file.
    /// </summary>
    /// <param name="folder">Folder path to save to.</param>
    /// <param name="content">Content to save.</param>
    /// <param name="fileName">File name to save to.</param>
    /// <param name="options">File creation options.</param>
    /// <returns>File.</returns>
    /// <exception cref="ArgumentNullException">Content saving is null.</exception>
    /// <exception cref="ArgumentException">File name is empty.</exception>
    public static async Task<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentException("File name is null or empty. Specify a valid file name", nameof(fileName));
        }

        var storageFile = await folder.CreateFileAsync(fileName, options);
        await FileIO.WriteBytesAsync(storageFile, content);
        return storageFile;
    }

    /// <summary>
    /// Read file to byte array.
    /// </summary>
    /// <param name="folder">Folder to read from.</param>
    /// <param name="fileName">Filename to read from.</param>
    /// <returns>Bytes.</returns>
    public static async Task<byte[]?> ReadFileAsync(this StorageFolder folder, string fileName)
    {
        var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

        if ((item != null) && item.IsOfType(StorageItemTypes.File))
        {
            var storageFile = await folder.GetFileAsync(fileName);
            var content = await storageFile.ReadBytesAsync();
            return content;
        }

        return null;
    }

    /// <summary>
    /// Read bytes from a file.
    /// </summary>
    /// <param name="file">File to read from.</param>
    /// <returns>Bytes.</returns>
    public static async Task<byte[]?> ReadBytesAsync(this StorageFile file)
    {
        if (file != null)
        {
            using IRandomAccessStream stream = await file.OpenReadAsync();
            using var reader = new DataReader(stream.GetInputStreamAt(0));
            await reader.LoadAsync((uint)stream.Size);
            var bytes = new byte[stream.Size];
            reader.ReadBytes(bytes);
            return bytes;
        }

        return null;
    }

    private static string GetFileName(string name)
    {
        return string.Concat(name, FileExtension);
    }
}
