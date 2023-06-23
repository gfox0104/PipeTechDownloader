// <copyright file="HttpHelper.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeTech.Downloader.Helpers;

/// <summary>
/// Helper class for the Http operations.
/// </summary>
public static class HttpHelper
{
    /// <summary>
    /// Minimum size for breaking up file downloads.
    /// </summary>
    public static readonly long MINSIZE = 5 * (long)Math.Pow(2, 20);

    /// <summary>
    /// Get uri header information.
    /// </summary>
    /// <param name="uri">Uri.</param>
    /// <param name="httpClient">Client.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    public static async Task<(long? Size, bool AcceptsRangeAsBytes, string? Etag)> GetUriInfo(
        Uri uri,
        HttpClient httpClient)
    {
        if (httpClient is null)
        {
            throw new ArgumentNullException(nameof(httpClient));
        }

        var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, uri));
        response.EnsureSuccessStatusCode();

        return (response.Content.Headers.ContentLength,
            response.Headers.AcceptRanges.Contains("bytes"),
            response.Headers.ETag?.Tag);
    }
}
