using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Link([property: JsonPropertyName("href")] string Href);