// <copyright file="GuidJsonConverter.cs" company="Industrial Technology Group">
// Copyright (c) Industrial Technology Group. All rights reserved.
// </copyright>

using System.Text.Json;
using System.Text.Json.Serialization;

namespace PipeTech.Downloader.Core.Helpers;

/// <summary>
/// Converter from Guid to json.
/// </summary>
public class GuidJsonConverter : JsonConverter<Guid>
{
    /// <inheritdoc/>
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        if (string.IsNullOrEmpty(valueString))
        {
            return Guid.Empty;
        }

        if (Guid.TryParse(valueString, out Guid value))
        {
            return value;
        }

        try
        {
            return Guid.ParseExact(valueString, "N");
        }
        catch (Exception)
        {
            return Guid.Empty;
        }
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
