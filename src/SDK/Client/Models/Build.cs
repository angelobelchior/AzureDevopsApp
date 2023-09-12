using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Build(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("buildNumber")] string BuildNumber,
    [property: JsonPropertyName("status")] BuildStatus BuildStatus,
    [property: JsonPropertyName("result")] BuildResultStatus Result,
    [property: JsonPropertyName("queueTime")] DateTime QueueTime,
    [property: JsonPropertyName("startTime")] DateTime StartTime,
    [property: JsonPropertyName("finishTime")] DateTime FinishTime,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("definition")] Definition Definition,
    [property: JsonPropertyName("buildNumberRevision")] int BuildNumberRevision,
    [property: JsonPropertyName("project")] Project Project,
    [property: JsonPropertyName("uri")] string Uri,
    [property: JsonPropertyName("sourceBranch")] string SourceBranch,
    [property: JsonPropertyName("sourceVersion")] string SourceVersion,
    [property: JsonPropertyName("queue")] Queue Queue,
    [property: JsonPropertyName("priority")] Priority Priority,
    [property: JsonPropertyName("reason")] Reason Reason,
    [property: JsonPropertyName("requestedFor")] Person RequestedFor,
    [property: JsonPropertyName("requestedBy")] Person RequestedBy,
    [property: JsonPropertyName("lastChangedDate")] DateTime LastChangedDate,
    [property: JsonPropertyName("lastChangedBy")] Person LastChangedBy,
    [property: JsonPropertyName("repository")] Repository Repository,
    [property: JsonPropertyName("keepForever")] bool KeepForever,
    [property: JsonPropertyName("retainedByRelease")] bool RetainedByRelease)
{
    public TimeSpan Duration => FinishTime - StartTime;
    public string BuildSourceInfo => CreateBuildSourceInfo();

    private string CreateBuildSourceInfo()
    {
        var parts = SourceBranch.Split('/');
        var branchName = parts.Last();
        var versionShort = string.Empty;
        if (!string.IsNullOrEmpty(SourceVersion))
            versionShort = SourceVersion[..7];
        return $"{branchName} {versionShort}";
    }
}