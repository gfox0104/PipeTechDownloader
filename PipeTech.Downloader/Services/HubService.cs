// <copyright file="HubService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Text.Json;
using Microsoft.Extensions.Logging;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Core.Models;
using PipeTech.Downloader.Helpers;
using Refit;
using Windows.ApplicationModel.AppService;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Hub communication service class.
/// </summary>
public class HubService : IHubService, IDisposable
{
    private readonly ILogger<HubService>? logger;
    private readonly HttpClient httpClient;
    private readonly IHttpClientFactory httpClientFactory;

    private IHubRefitService? hubRefit;

    /// <summary>
    /// Initializes a new instance of the <see cref="HubService"/> class.
    /// </summary>
    /// <param name="client">Http client.</param>
    /// <param name="httpClientFactory">HttpClient factory service.</param>
    /// <param name="logger">Logger service.</param>
    public HubService(
            HttpClient client,
            IHttpClientFactory httpClientFactory,
            ILogger<HubService>? logger = null)
    {
        this.httpClient = client;
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this.httpClient?.Dispose();
    }

    /// <inheritdoc/>
    public async Task<Manifest?> GetManifest(string id, CancellationToken token = default)
    {
        if (this.hubRefit is null)
        {
            throw new Exception($"{nameof(this.SetBaseAddress)} needs to be called first.");
        }

        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        var response = await this.hubRefit.GetManifest(id, token);
        if (response == null)
        {
            return null;
        }

        if (response.Error is not null)
        {
            throw response.Error;
        }

        return response.Content;
    }

    /// <inheritdoc/>
    public async Task<Manifest?> GetManifest(Uri link, CancellationToken token = default)
    {
        if (this.hubRefit is null)
        {
            throw new Exception($"{nameof(this.SetBaseAddress)} needs to be called first.");
        }

        try
        {
            using var client = this.httpClientFactory.CreateClient();

            var (size, acceptsRange, etag) = default((long?, bool, string?));
            try
            {
                (size, acceptsRange, etag) = await HttpHelper.GetUriInfo(link, client);
            }
            catch (Exception ex)
            {
                this.logger?.LogInformation(ex, $"Manifest uri does not support retrieving headers [{link.OriginalString}]");
            }

            using var ms = new MemoryStream();

            if (size is null || size <= HttpHelper.MINSIZE || !acceptsRange)
            {
                // Do it in one part
                using var response = await client.SendAsync(
                    new HttpRequestMessage(HttpMethod.Get, link),
                    token);
                response.EnsureSuccessStatusCode();
                response.Content.ReadAsStream().CopyTo(ms);
            }
            else
            {
                // Break it up
                var byteCount = 0L;
                while (byteCount < size)
                {
                    GC.Collect();

                    if (token.IsCancellationRequested)
                    {
                        throw new TaskCanceledException();
                    }

                    var workingSize = Math.Min(HttpHelper.MINSIZE, size.Value - byteCount);

                    var request = new HttpRequestMessage(HttpMethod.Get, link);
                    request.Headers.Range =
                    new System.Net.Http.Headers.RangeHeaderValue(
                        byteCount, byteCount + (workingSize - 1));
                    var response = await client.SendAsync(request, token);
                    response.EnsureSuccessStatusCode();
                    response.Content.ReadAsStream().CopyTo(ms);

                    byteCount += workingSize;
                }
            }

            ms.Seek(0, SeekOrigin.Begin);

            if (ms.Length <= 0)
            {
                // No data received.
                throw new InvalidDataException("No data received.");
            }

            using var sr = new StreamReader(ms);
            var json = sr.ReadToEnd();

            return JsonSerializer.Deserialize<Manifest?>(
                json,
                new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error getting manifest.");
            return null;
        }
    }

    /// <inheritdoc/>
    public async Task<Uri?> GetManifestLink(string id, CancellationToken token = default)
    {
        if (this.hubRefit is null)
        {
            throw new Exception($"{nameof(this.SetBaseAddress)} needs to be called first.");
        }

        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        try
        {
            var response = await this.hubRefit.GetManifestLink(id, token);
            if (response == null)
            {
                return null;
            }

            if (response.Error is not null)
            {
                throw response.Error;
            }

            if (string.IsNullOrEmpty(response.Content))
            {
                throw new NullReferenceException("Empty content.");
            }

            var element = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(response.Content);
            if (!element.TryGetProperty("manifestURI", out var manifestUri) ||
                !Uri.TryCreate(manifestUri.GetString() ?? string.Empty, UriKind.RelativeOrAbsolute, out var uri))
            {
                return null;
            }

            return uri;
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error getting manifest link");
            return null;
        }
    }

    /// <inheritdoc/>
    public void SetBaseAddress(Uri baseAddress)
    {
        if (this.hubRefit is IDisposable d)
        {
            d.Dispose();
        }

        this.hubRefit = null;

        try
        {
            this.httpClient.BaseAddress = baseAddress; // new(@"https://api.pipetechproject.com");
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, $"Error setting base address of hub communication");
        }

        this.hubRefit = RestService.For<IHubRefitService>(
            this.httpClient,
            new RefitSettings(
                new SystemTextJsonContentSerializer(new()
                {
                    AllowTrailingCommas = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                })));
    }
}
