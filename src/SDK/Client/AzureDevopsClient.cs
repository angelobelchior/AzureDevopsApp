using AzureDevops.Client.Services;
using AzureDevops.Client.Services.Builds;
using AzureDevops.Client.Services.Definitions;
using AzureDevops.Client.Services.Projects;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using AzureDevops.Client.Models;

namespace AzureDevops.Client;

public class AzureDevopsClient : IAzureDevopsClient
{
    private IProjects? _projects;
    public IProjects Projects => _projects ??= new Projects(_httpClient, _asyncRetryPolicy);

    private IBuilds? _builds;
    public IBuilds Builds => _builds ??= new Builds(_httpClient, _asyncRetryPolicy);

    private IDefinitions? _definitions;
    public IDefinitions Definitions => _definitions ??= new Definitions(_httpClient, _asyncRetryPolicy);

    private HttpClient _httpClient;
    private AsyncRetryPolicy _asyncRetryPolicy;
    private AzureDevopsClient(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
    {
        _httpClient = httpClient;
        _asyncRetryPolicy = asyncRetryPolicy;
    }
    
    public static IAzureDevopsClient Create(string organization, string personalAccessToken,
        RetryPolicyConfiguration retryPolicyConfiguration)
    {
        var httpClient = CreateHttpClient(organization, personalAccessToken);
        var asyncRetryPolicy = CreatePolicy(retryPolicyConfiguration);
        
        return new AzureDevopsClient(httpClient, asyncRetryPolicy);
    }

    private static AsyncRetryPolicy CreatePolicy(RetryPolicyConfiguration retryPolicyConfiguration)
        => Policy.Handle<Exception>(e => true)
            .WaitAndRetryAsync(retryCount: retryPolicyConfiguration.RetryCount,
                retryAttempt => TimeSpan.FromSeconds(retryPolicyConfiguration.RetryAttemptFactor * retryAttempt)
            );

    private static HttpClient CreateHttpClient(string organization, string personalAccessToken)
    {
        var baseUrl = $"https://dev.azure.com/{organization}/";
        var client = new HttpClient();
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.ConnectionClose = false;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Authorization = CreateAuthenticationHeaderValue(personalAccessToken);
        return client;
    }

    private static AuthenticationHeaderValue CreateAuthenticationHeaderValue(string personalAccessToken)
    {
        var byteArray = Encoding.ASCII.GetBytes($":{personalAccessToken}");
        var base64 = Convert.ToBase64String(byteArray);
        return new AuthenticationHeaderValue("Basic", base64);
    }
}