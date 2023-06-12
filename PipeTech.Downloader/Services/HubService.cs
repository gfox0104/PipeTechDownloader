// <copyright file="HubService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.Extensions.Logging;
using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Core.Models;
using Refit;
using Windows.ApplicationModel.AppService;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Hub communication service class.
/// </summary>
public class HubService : IHubService
{
    private readonly ILogger<HubService>? logger;
    private readonly HttpClient httpClient;

    private IHubRefitService? hubRefit;

    /// <summary>
    /// Initializes a new instance of the <see cref="HubService"/> class.
    /// </summary>
    /// <param name="client">Http client.</param>
    /// <param name="logger">Logger service.</param>
    public HubService(
            HttpClient client,
            ILogger<HubService>? logger = null)
    {
        this.httpClient = client;
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async Task<Manifest?> GetManifest(Guid id, CancellationToken token = default)
    {
        if (this.hubRefit is null)
        {
            throw new Exception($"{nameof(this.SetBaseAddress)} needs to be called first.");
        }

        if (id == Guid.Empty)
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
                    PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
                })));
    }
}
