using System;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Project(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("description")] string Description,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("visibility")] ProjectVisibility Visibility,
    [property: JsonPropertyName("lastUpdateTime")] DateTime LastUpdateTime);