using System;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Change(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("location")] string Location,
    [property: JsonPropertyName("message")] string Message,
    [property: JsonPropertyName("messageTruncated")] string MessageTruncated,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("pusher")] string Pusher,
    [property: JsonPropertyName("author")] Person Author,
    [property: JsonPropertyName("timeStamp")] DateTime TimeStamp);