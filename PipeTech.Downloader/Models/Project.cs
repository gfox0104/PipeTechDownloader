// <copyright file="Project.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using PipeTech.Downloader.Core.Contracts;

namespace PipeTech.Downloader.Models;

/// <summary>
/// Project class.
/// </summary>
public partial class Project : BindableRecipient, IManifest
{
    [ObservableProperty]
    private ObservableCollection<DownloadInspectionHandler>? inspections;

    [ObservableProperty]
    private string? name;

    [ObservableProperty]
    private DateTime? confirmationTime;

    [ObservableProperty]
    [JsonIgnore]
    private bool expanded = false;

    [ObservableProperty]
    private string? downloadPath;

    [ObservableProperty]
    private Guid id;

    /// <summary>
    /// Gets or sets the file path.
    /// </summary>
    [ObservableProperty]
    private string? filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    public Project()
    {
    }

    /// <summary>
    /// Gets the progress.
    /// </summary>
    [JsonIgnore]
    public decimal Progress => this.Inspections?.Sum(h => h.Inspection?.Progress ?? 0) ?? 0 / this.Inspections?.Count ?? 0;

    /// <summary>
    /// Gets the progress.
    /// </summary>
    [JsonIgnore]
    public long TotalSize => this.Inspections?.Sum(h => h.Inspection?.TotalSize ?? 0) ?? 0;

    /// <inheritdoc/>
    public bool? IndividualNASSCOExchangeGenerate
    {
        get; set;
    }

    /// <inheritdoc/>
    public bool? CombinedNASSCOExchangeGenerate
    {
        get; set;
    }

    /// <inheritdoc/>
    public Guid[]? CombinedReportIds
    {
        get; set;
    }

    /// <inheritdoc/>
    public Guid[]? IndividualReportIds
    {
        get; set;
    }

    /// <inheritdoc/>
    public string? DeliverableName
    {
        get; set;
    }

    /// <inheritdoc/>
    [JsonExtensionData]
    public Dictionary<string, object?>? AdditionalProperties
    {
        get; set;
    }

    /// <summary>
    /// Create <see cref="Project"/> from json.
    /// </summary>
    /// <param name="json">Json to create <see cref="Project"/> from.</param>
    /// <returns><see cref="Project"/>.</returns>
    public static Project FromJson(string json)
    {
        var p = App.GetService<Project>();
        if (!string.IsNullOrEmpty(json))
        {
            var ele = System.Text.Json.JsonSerializer.Deserialize<JsonElement?>(json);
            if (ele is not null)
            {
                if (ele.Value.TryGetProperty(nameof(Project.Name), out var name))
                {
                    p.Name = name.ToString();
                }

                if (ele.Value.TryGetProperty(nameof(Project.ConfirmationTime), out var ct) &&
                    DateTime.TryParse(ct.ToString(), out var dt))
                {
                    p.ConfirmationTime = dt;
                }

                if (ele.Value.TryGetProperty(nameof(Project.DownloadPath), out var dp))
                {
                    p.DownloadPath = dp.ToString();
                }

                if (ele.Value.TryGetProperty(nameof(Project.Id), out var id) &&
                    Guid.TryParse(id.ToString(), out var g))
                {
                    p.Id = g;
                }

                if (ele.Value.TryGetProperty(nameof(Project.DeliverableName), out var dName))
                {
                    p.DeliverableName = dName.ToString();
                }

                if (ele.Value.TryGetProperty(nameof(Project.AdditionalProperties), out var additionalProps))
                {
                    p.AdditionalProperties = JsonSerializer.Deserialize<Dictionary<string, object?>?>(additionalProps.ToString());
                }

                if (ele.Value.TryGetProperty(nameof(Project.CombinedNASSCOExchangeGenerate), out var combineGen) &&
                    bool.TryParse(combineGen.ToString(), out var b))
                {
                    p.CombinedNASSCOExchangeGenerate = b;
                }

                if (ele.Value.TryGetProperty(nameof(Project.IndividualNASSCOExchangeGenerate), out var individualGen) &&
                    bool.TryParse(individualGen.ToString(), out var bI))
                {
                    p.IndividualNASSCOExchangeGenerate = bI;
                }

                if (ele.Value.TryGetProperty(nameof(Project.CombinedReportIds), out var combineReportIds))
                {
                    p.CombinedReportIds = JsonSerializer.Deserialize<Guid[]?>(combineReportIds);
                }

                if (ele.Value.TryGetProperty(nameof(Project.IndividualReportIds), out var individualReportIds))
                {
                    p.IndividualReportIds = JsonSerializer.Deserialize<Guid[]?>(individualReportIds);
                }

                try
                {
                    if (ele.Value.TryGetProperty(nameof(Project.Inspections), out var insp))
                    {
                        var inspections = insp.EnumerateArray()
                            .Cast<JsonElement?>()
                            .Where(j => j is not null)
                            .Select(je => JsonSerializer.Deserialize<DownloadInspection?>(je!.ToString() ?? string.Empty));

                        p.Inspections ??= new();
                        p.Inspections!.AddRange(inspections.Select(i =>
                        {
                            var dlh = App.GetService<DownloadInspectionHandler>();
                            dlh.BypassLoad = true;
                            dlh.Inspection = i;
                            dlh.BypassLoad = false;
                            return dlh;
                        }));
                    }
                }
                catch (InvalidCastException)
                {
                }
                catch (Exception)
                {
                }
            }
        }

        return p;
    }

    /// <summary>
    /// Get Json of this <see cref="Project"/>.
    /// </summary>
    /// <returns>Json string.</returns>
    public string ToJson()
    {
        return JsonSerializer.Serialize(new
        {
            this.Id,
            this.Name,
            this.ConfirmationTime,
            this.DownloadPath,
            this.DeliverableName,
            this.AdditionalProperties,
            this.CombinedNASSCOExchangeGenerate,
            this.CombinedReportIds,
            this.IndividualReportIds,
            this.IndividualNASSCOExchangeGenerate,
            Inspections = this.Inspections?.Select(i => i.Inspection),
        });
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanging(PropertyChangingEventArgs e)
    {
        base.OnPropertyChanging(e);
        switch (e.PropertyName)
        {
            case nameof(this.Inspections):
                if (this.Inspections is not null)
                {
                    this.Inspections.CollectionChanged -= this.Inspections_CollectionChanged;
                }

                break;
            default:
                break;
        }
    }

    /// <inheritdoc/>
    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        switch (e.PropertyName)
        {
            case nameof(this.Inspections):
                if (this.Inspections is not null)
                {
                    this.Inspections.CollectionChanged += this.Inspections_CollectionChanged;
                }

                break;
            default:
                break;
        }
    }

    private void Inspections_CollectionChanged(
        object? sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (var oldItem in e.OldItems)
            {
                if (oldItem is DownloadInspectionHandler dlh)
                {
                    dlh.PropertyChanging -= this.DownloadInspectionHandler_PropertyChanging;
                    dlh.PropertyChanged -= this.DownloadInspectionHandler_PropertyChanged;

                    if (dlh.Inspection is not null)
                    {
                        dlh.Inspection.PropertyChanging -= this.Inspection_PropertyChanging;
                        dlh.Inspection.PropertyChanged -= this.Inspection_PropertyChanged;
                    }
                }
            }
        }

        if (e.NewItems is not null)
        {
            foreach (var newItem in e.NewItems)
            {
                if (newItem is DownloadInspectionHandler dlh)
                {
                    dlh.PropertyChanging += this.DownloadInspectionHandler_PropertyChanging;
                    dlh.PropertyChanged += this.DownloadInspectionHandler_PropertyChanged;

                    if (dlh.Inspection is not null)
                    {
                        dlh.Inspection.PropertyChanging += this.Inspection_PropertyChanging;
                        dlh.Inspection.PropertyChanged += this.Inspection_PropertyChanged;
                    }
                }
            }
        }

        this.RaisePropertyChanged(nameof(this.Progress));
        this.RaisePropertyChanged(nameof(this.TotalSize));
    }

    private void Inspection_PropertyChanging(
        object? sender,
        System.ComponentModel.PropertyChangingEventArgs e)
    {
    }

    private void Inspection_PropertyChanged(
        object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspection.Progress):
                this.RaisePropertyChanged(nameof(this.Progress));
                break;
            case nameof(DownloadInspection.TotalSize):
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            default:
                break;
        }
    }

    private void DownloadInspectionHandler_PropertyChanged(
        object? sender,
        System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (sender is DownloadInspectionHandler dlh &&
                    dlh.Inspection is not null)
                {
                    dlh.Inspection.PropertyChanging += this.DownloadInspectionHandler_PropertyChanging;
                    dlh.Inspection.PropertyChanged += this.DownloadInspectionHandler_PropertyChanged;
                }

                this.RaisePropertyChanged(nameof(this.Progress));
                this.RaisePropertyChanged(nameof(this.TotalSize));
                break;
            default:
                break;
        }
    }

    private void DownloadInspectionHandler_PropertyChanging(
        object? sender,
        System.ComponentModel.PropertyChangingEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(DownloadInspectionHandler.Inspection):
                if (sender is DownloadInspectionHandler dlh &&
                    dlh.Inspection is not null)
                {
                    dlh.Inspection.PropertyChanging -= this.DownloadInspectionHandler_PropertyChanging;
                    dlh.Inspection.PropertyChanged -= this.DownloadInspectionHandler_PropertyChanged;
                }

                break;
            default:
                break;
        }
    }
}
