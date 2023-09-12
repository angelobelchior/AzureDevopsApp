using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Log(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("type")] string Type, 
    [property: JsonPropertyName("url")] string Url);