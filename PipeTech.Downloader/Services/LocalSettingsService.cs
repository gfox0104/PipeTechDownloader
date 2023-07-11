// <copyright file="LocalSettingsService.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using Microsoft.Extensions.Options;

using PipeTech.Downloader.Contracts.Services;
using PipeTech.Downloader.Core.Contracts.Services;
using PipeTech.Downloader.Core.Helpers;
using PipeTech.Downloader.Helpers;
using PipeTech.Downloader.Models;

using Windows.Storage;

namespace PipeTech.Downloader.Services;

/// <summary>
/// Local settings service class.
/// </summary>
public class LocalSettingsService : ILocalSettingsService
{
    private const string DefaultApplicationDataFolder = "PipeTech.Downloader/ApplicationData";
    private const string DefaultLocalSettingsFile = "LocalSettings.json";

    private readonly IFileService fileService;
    private readonly LocalSettingsOptions options;

    private readonly string localApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string applicationDataFolder;
    private readonly string localSettingsFile;

    private IDictionary<string, object> settings;

    private bool isInitialized;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalSettingsService"/> class.
    /// </summary>
    /// <param name="fileService">File service.</param>
    /// <param name="options">Options.</param>
    public LocalSettingsService(IFileService fileService, IOptions<LocalSettingsOptions> options)
    {
        this.fileService = fileService;
        this.options = options.Value;

        this.applicationDataFolder = Path.Combine(
            this.localApplicationData,
            this.options.ApplicationDataFolder ?? DefaultApplicationDataFolder);
        this.localSettingsFile = this.options.LocalSettingsFile ?? DefaultLocalSettingsFile;

        this.settings = new Dictionary<string, object>();
    }

    /// <inheritdoc/>
    public async Task<T?> ReadSettingAsync<T>(string key)
    {
        try
        {
            if (RuntimeHelper.IsMSIX)
            {
                if (ApplicationData.Current.LocalSettings.Values.TryGetValue(key, out var obj))
                {
                    return await Json.ToObjectAsync<T>((string)obj);
                }
            }
            else
            {
                await this.InitializeAsync();

                if (this.settings != null && this.settings.TryGetValue(key, out var obj))
                {
                    return await Json.ToObjectAsync<T>(obj.ToString() ?? string.Empty);
                }
            }
        }
        catch (Exception)
        {
        }

        return default;
    }

    /// <inheritdoc/>
    public async Task SaveSettingAsync<T>(string key, T value)
    {
        if (RuntimeHelper.IsMSIX)
        {
            ApplicationData.Current.LocalSettings.Values[key] = await Json.StringifyAsync(value);
            ////await ApplicationData.Current.LocalSettings.SaveAsync(key, value);
        }
        else
        {
            await this.InitializeAsync();

            this.settings[key] = await Json.StringifyAsync(value);

            await Task.Run(() => this.fileService.Save(
                this.applicationDataFolder,
                this.localSettingsFile,
                this.settings));
        }
    }

    private async Task InitializeAsync()
    {
        if (!this.isInitialized)
        {
            this.settings = await Task.Run(() => this.fileService.Read<IDictionary<string, object>>(
                this.applicationDataFolder,
                this.localSettingsFile)) ?? new Dictionary<string, object>();

            this.isInitialized = true;
        }
    }
}
