// <copyright file="IFileService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

namespace PipeTech.Downloader.Core.Contracts.Services;

/// <summary>
/// File service interface.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Read from file.
    /// </summary>
    /// <typeparam name="T">Type of the read.</typeparam>
    /// <param name="folderPath">Folder path.</param>
    /// <param name="fileName">File name.</param>
    /// <returns>Object read.</returns>
    T? Read<T>(string folderPath, string fileName);

    /// <summary>
    /// Save to file.
    /// </summary>
    /// <typeparam name="T">Object to save to file.</typeparam>
    /// <param name="folderPath">Folder to save to.</param>
    /// <param name="fileName">File name to save to.</param>
    /// <param name="content">Content to save.</param>
    void Save<T>(string folderPath, string fileName, T content);

    /// <summary>
    /// Delete file.
    /// </summary>
    /// <param name="folderPath">Folder path to delete file from.</param>
    /// <param name="fileName">File to delete.</param>
    void Delete(string folderPath, string fileName);
}
