using System;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record TimelineRecord(
    [property: JsonPropertyName("id")] Guid Id,
    [property: JsonPropertyName("parentId")] Guid? ParentId,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("startTime")]  DateTime StartTime,
    [property: JsonPropertyName("finishTime")] DateTime FinishTime,
    [property: JsonPropertyName("state")] TimelineRecordState State,
    [property: JsonPropertyName("result")] TaskResult Result,
    [property: JsonPropertyName("lastModified")] DateTime LastModified,
    [property: JsonPropertyName("workerName")] string WorkerName,
    [property: JsonPropertyName("order")] int Order,
    [property: JsonPropertyName("errorCount")] int ErrorCount,
    [property: JsonPropertyName("warningCount")] int WarningCount,
    [property: JsonPropertyName("details")] string Details,
    [property: JsonPropertyName("log")] Log Log,
    [property: JsonPropertyName("attempt")] int Attempt,
    [property: JsonPropertyName("identifier")] string Identifier)
{
    public TimeSpan Duration => FinishTime - StartTime;
}