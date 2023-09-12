using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Queue(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("pool")] Pool Pool);