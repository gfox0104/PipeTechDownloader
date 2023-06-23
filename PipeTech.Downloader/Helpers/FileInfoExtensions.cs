// <copyright file="FileInfoExtensions.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Security.Cryptography;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// <see cref="FileInfo"/> extensions class.
/// </summary>
public static class FileInfoExtensions
{
    /// <summary>
    /// Calculate MD5 hash for a file.
    /// </summary>
    /// <param name="fileInfo">File info.</param>
    /// <returns>MD5 hash.</returns>
    public static byte[]? MD5FileHash(this FileInfo fileInfo)
    {
        if (!fileInfo.Exists)
        {
            return null;
        }

        using var md5 = MD5.Create();
        using var sr = fileInfo.OpenRead();
        return md5?.ComputeHash(sr);
    }

    /// <summary>
    /// Calculate MD5 hash for a file and convert it to base 64.
    /// </summary>
    /// <param name="fileInfo">File info.</param>
    /// <returns>MD5 hash as base 64 string.</returns>
    public static string MD5FileBase64Hash(this FileInfo fileInfo)
    {
        return Convert.ToBase64String(MD5FileHash(fileInfo) ?? Array.Empty<byte>());
    }

    /// <summary>
    /// Calculate MD5 hash for a file and convert it to hex.
    /// </summary>
    /// <param name="fileInfo">File info.</param>
    /// <returns>MD5 hash as base 64 string.</returns>
    public static string MD5FileHexHash(this FileInfo fileInfo)
    {
        return BitConverter.ToString(MD5FileHash(fileInfo) ?? Array.Empty<byte>()).Replace("-", string.Empty);
    }
}
