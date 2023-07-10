// <copyright file="Manifest.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;
using PipeTech.Downloader.Core.Contracts;
using PipeTech.Downloader.Core.Helpers;

namespace PipeTech.Downloader.Core.Models;

/// <summary>
/// Manifest class.
/// </summary>
public class Manifest : IManifest
{
    /// <summary>
    /// Gets or sets the Id of the manifest.
    /// </summary>
    public string? Id
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the inspections in the manifest.
    /// </summary>
    public JsonElement[]? Inspections
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to generate NASSCO exchange databases individually for each of the inspections.
    /// </summary>
    public bool? IndividualNASSCOExchangeGenerate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to generate a NASSCO exchange databases as combined for all inspections.
    /// </summary>
    public bool? CombinedNASSCOExchangeGenerate
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the combined report ids of the report to generate for the inspections.
    /// </summary>
    public Guid[]? CombinedReportIds
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the individual report ids of the report to generate for the inspections.
    /// </summary>
    public Guid[]? IndividualReportIds
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets the deliverable name.
    /// </summary>
    public string? DeliverableName
    {
        get; set;
    }

    /// <summary>
    /// Gets or sets additional properties.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object?>? AdditionalProperties
    {
        get; set;
    }
}
