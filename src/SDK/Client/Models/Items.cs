using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Items<T>(
    [property:JsonPropertyName("count")] int Count,
    [property:JsonPropertyName("value")] IEnumerable<T> Value);