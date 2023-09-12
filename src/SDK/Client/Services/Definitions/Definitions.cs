using Polly.Retry;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Definitions;

internal class Definitions : ServiceBase, IDefinitions
{
    public Definitions(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
        : base(httpClient, asyncRetryPolicy) { }

    public async Task<Result<Items<Definition>>> ListAll(string projectName)
    {
        var url = $"{projectName}/_apis/build/definitions?api-version=5.1";
        return await Get<Items<Definition>>(url);
    }
}