using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Pool(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("isHosted")] bool IsHosted);