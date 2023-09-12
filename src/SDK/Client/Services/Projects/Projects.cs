using Polly.Retry;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Projects;

internal class Projects : ServiceBase, IProjects
{
    public Projects(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
        : base(httpClient, asyncRetryPolicy) { }

    public async Task<Result<Items<Project>>> ListAll()
    {
        var url = "_apis/projects?api-version=5.1";
        return await Get<Items<Project>>(url);
    }
}