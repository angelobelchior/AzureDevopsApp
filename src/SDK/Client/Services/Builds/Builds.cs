using Polly.Retry;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Builds;

internal class Builds : ServiceBase, IBuilds
{
    public Builds(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
        : base(httpClient, asyncRetryPolicy) { }

    public async Task<Result<Items<Build>>> ListAll(string projectName, int? definitionId = null)
    {
        var filter = string.Empty;
        if (definitionId.HasValue)
            filter = $"definitions={definitionId}&";
        var url = $"{projectName}/_apis/build/builds?{filter}api-version=5.1";
        return await Get<Items<Build>>(url);
    }

    public async Task<Result<Build>> Queue(Definition definition)
    {
        var url = $"{definition.Project.Name}/_apis/build/builds?api-version=5.1";
        var request = new { definition = new { id = definition.Id } };
        return await Post<Build>(url, request);
    }

    public async Task<Result<Items<Change>>> ListChanges(string projectName, int buildId)
    {
        var url = $"{projectName}/_apis/build/builds/{buildId}/changes?api-version=5.1";
        return await Get<Items<Change>>(url);
    }

    public async Task<Result<Timeline>> GetTimeline(string projectName, int buildId)
    {
        var url = $"{projectName}/_apis/build/builds/{buildId}/timeline/?api-version=5.0";
        return await Get<Timeline>(url);
    }

    public async Task<Result<Items<string>>> GetLogs(string projectName, int buildId, int logId)
    {
        var url = $"{projectName}/_apis/build/builds/{buildId}/logs/{logId}?api-version=5.1";
        return await Get<Items<string>>(url);
    }
}