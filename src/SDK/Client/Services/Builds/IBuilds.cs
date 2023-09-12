using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Builds;

public interface IBuilds
{
    Task<Result<Items<Build>>> ListAll(string projectName, int? definitionId = null);
    Task<Result<Items<Change>>> ListChanges(string projectName, int buildId);
    Task<Result<Items<string>>> GetLogs(string projectName, int buildId, int logId);
    Task<Result<Timeline>> GetTimeline(string projectName, int buildId);

    Task<Result<Build>> Queue(Definition definition);
}