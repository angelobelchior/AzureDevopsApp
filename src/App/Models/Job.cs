using AzureDevops.Client.Models;

namespace AzureDevops.Models;

public class Job
{
    public Guid Id { get; set; }
    public int Depth { get; set; }
    public string Name { get; set; }
    public TimeSpan Duration { get; set; }
    public int Order { get; set; }
    public TaskResult Result { get; set; }
    public int? LogId { get; set; }

    public Job(TimelineRecord record, int depth)
    {
        Depth = depth;
        Id = record.Id;
        Name = record.Name;
        Duration = record.Duration;
        Order = record.Order;
        Result = record.Result;
        LogId = record.Log?.Id;
    }

    public static IList<Job> CreateJobs(TimelineRecord[] records)
    {
        var jobs = new List<Job>();

        var recordDictionary = records.ToLookup(r => r.ParentId);

        var root = records.FirstOrDefault(r => r.ParentId == null);
        if (root != null)
            AddJobsRecursively(root, 1);

        return jobs;

        void AddJobsRecursively(TimelineRecord record, int depth)
        {
            jobs.Add(new Job(record, depth));
            foreach (var childRecord in recordDictionary[record.Id].OrderBy(r => r.Order))
                AddJobsRecursively(childRecord, depth + 1);
        }
    }
}