using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Definition(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("project")] Project Project);