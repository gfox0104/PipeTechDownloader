// <copyright file="DownloadInspection.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using PipeTech.Downloader.Contracts.Services;
using PT.Inspection;
using PT.Inspection.Inspections;
using PT.Inspection.Model;
using PT.Inspection.Packs;
using PT.Inspection.Templates;
using PT.Inspection.Wpf;
using Refit;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Download inspection class.
/// </summary>
public partial class DownloadInspection : ObservableRecipient
{
    private readonly ILogger<DownloadInspection>? logger;
    private readonly IServiceProvider serviceProvider;

    [ObservableProperty]
    private States state;

    [ObservableProperty]
    private long? totalSize;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private string? project;

    [ObservableProperty]
    private string? downloadPath;

    [ObservableProperty]
    private long size = 0;

    [ObservableProperty]
    private decimal progress = 0;

    [ObservableProperty]
    private bool expanded = false;

    private string? json;

    /// <summary>
    /// Initializes a new instance of the <see cref="DownloadInspection"/> class.
    /// </summary>
    /// <param name="serviceProvider">Service provider.</param>
    public DownloadInspection(
        IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.logger = serviceProvider.GetService(typeof(ILogger<DownloadInspection>)) as ILogger<DownloadInspection>;
        this.Files = new();
    }

    /// <summary>
    /// States of the download.
    /// </summary>
    public enum States
    {
        /// <summary>
        /// Loading.
        /// </summary>
        Loading,

        /// <summary>
        /// Error.
        /// </summary>
        Errored,

        /// <summary>
        /// Complete.
        /// </summary>
        Complete,
    }

    /// <summary>
    /// Gets the files.
    /// </summary>
    public ObservableCollection<string> Files
    {
        get;
    }

    /// <summary>
    /// Gets or sets the json.
    /// </summary>
    public string? Json
    {
        get => this.json;
        set => this.SetProperty(ref this.json, value);
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        switch (e.PropertyName)
        {
            case nameof(this.Json):
                _ = this.LoadInspection();
                break;
            default:
                break;
        }
    }

    private async Task LoadInspection()
    {
        await Task.Yield();
        this.State = States.Loading;
        try
        {
            if (this.Json is null ||
                string.IsNullOrEmpty(this.Json))
            {
                throw new NullReferenceException($"{nameof(this.Json)} is empty.");
            }

            var ele = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(this.Json);
            if (!ele.TryGetProperty("$packId", out var value) ||
                !Guid.TryParse(value.ToString(), out var packId))
            {
                throw new Exception($"Unable to retrieve pack Id of inspection.");
            }

            var tm = this.serviceProvider.GetService(typeof(ITemplateRegistry)) as ITemplateRegistry;
            var pack = tm?.InstalledTemplates?
                .Where(p => p.Metadata.ID == packId)
                .OrderByDescending(p => p.Metadata.Version)
                .FirstOrDefault();

            if (pack is null)
            {
                // Get the pack from external services.
                using var httpClient = this.serviceProvider.GetService(typeof(HttpClient)) as HttpClient;
                if (httpClient is not null)
                {
                    httpClient.BaseAddress = new(@"http://100.24.161.59:5000");
                }

                var externalServices = RestService.For<IExternalServices>(
                     httpClient,
                     new RefitSettings(
                         new SystemTextJsonContentSerializer(new()
                         {
                             AllowTrailingCommas = true,
                             PropertyNameCaseInsensitive = true,
                             PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                         })));

                var tempFile = PathWrapper.GetTempFileNameUnique();
                try
                {
                    var info = await externalServices.GetPack(packId, descending: true);
                    if (info.Content is null)
                    {
                        throw new Exception("Unable to download pack");
                    }

                    using (var fs = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite))
                    {
                        info.Content.CopyTo(fs);
                    }

                    var pf = this.serviceProvider.GetService(typeof(IPackFactory)) as IPackFactory;
                    pack = pf?.OpenPackFile(tempFile);
                    tm?.Register(pack, TemplateRegistryKnownLocationType.Machine);
                }
                finally
                {
                    if (externalServices is IDisposable d)
                    {
                        d.Dispose();
                    }

                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                }
            }

            var deserilizer = this.serviceProvider.GetService(typeof(IJsonDeserializerV2)) as IJsonDeserializerV2;
            using var ds = deserilizer?.Deserialize(pack, this.Json, false);

            if (ds is null)
            {
                throw new Exception($"Unable to deserialize inspection.");
            }

            if (!ds.TryGetInspectionRow(out var drInspection) || drInspection is null)
            {
                throw new Exception($"Unable to deserialize an inspection.");
            }

            var inspectionFactory = this.serviceProvider.GetService(typeof(IInspectionFactory)) as IInspectionFactory;
            using var inspection = inspectionFactory?.InitializeInspection(pack, ds, drInspection.Table.TableName) as Inspection;

            inspection?.AssetCollection.View.MoveCurrentToFirst();
            inspection?.InspectionCollection.View.MoveCurrentToFirst();

            var grouping = inspection?.ResolveInspectionFolderGroup();
            if (grouping?.Count > 0)
            {
                this.Name = string.Join("/", grouping.Select(n => n.SanitizeFilename()));
            }
            else
            {
                this.Name = inspection?.ResolveInspectionFolderName().SanitizeFilename();
            }

            var filename = inspection.ResolveInspectionFileNameWithoutExtension();
            this.Files.Add(filename + ".ptdx");

            if (inspection?.MediaReferences is not null)
            {
                foreach (var media in inspection.MediaReferences)
                {
                    if (media.ExistsLocal())
                    {
                        this.Files.Add(media.GetName());
                        this.TotalSize = (this.TotalSize ?? 0) + new FileInfo(media.GetAbsoluteUri().LocalPath).Length;
                    }
                    else
                    {
                        this.Files.Add(media.URI.OriginalString);
                    }
                }
            }

            this.State = States.Complete;
        }
        catch (Exception ex)
        {
            this.logger?.LogError(ex, "Error loading download inspection.");
            this.State = States.Errored;
        }
    }
}
