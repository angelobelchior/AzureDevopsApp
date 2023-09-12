using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AzureDevops.Client.Models;

public record Timeline(
    [property: JsonPropertyName("records")] TimelineRecord[] Records)
{
    public IEnumerable<TimelineRecord> GetJobs()
    {
        var timelineRecords = new List<TimelineRecord>();
        var root = Records.FirstOrDefault(r => r.ParentId == null);
        if (root is null) return Enumerable.Empty<TimelineRecord>();
        var parents = Records.Where(r => r.ParentId == root.Id)
            .OrderBy(r => r.Order);
        return parents;
    }
}