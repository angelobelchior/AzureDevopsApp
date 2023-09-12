using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Person(
    [property: JsonPropertyName("displayName")] string DisplayName,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("_links")] Links Links,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("uniqueName")] string UniqueName,
    [property: JsonPropertyName("imageUrl")] string ImageUrl,
    [property: JsonPropertyName("descriptor")] string Descriptor);