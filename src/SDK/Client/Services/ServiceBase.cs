using System;
using Polly.Retry;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services;

public abstract class ServiceBase
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = CreateJsonSerializerOptions();
    
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy _asyncRetryPolicy;
    public ServiceBase(HttpClient httpClient, AsyncRetryPolicy asyncRetryPolicy)
    {
        _httpClient = httpClient;
        _asyncRetryPolicy = asyncRetryPolicy;
    }

    protected async Task<Result<T>> Get<T>(string url)
    {
        return await _asyncRetryPolicy.ExecuteAsync(async () =>
        {
            var response = await _httpClient.GetAsync(url);
            return await GetRequestContent<T>(response);
        });
    }

    protected async Task<Result<T>> Post<T>(string url, object request)
        => await Post<T, object>(url, request);

    protected async Task<Result<T>> Post<T, TR>(string url, TR request)
    {
        return await _asyncRetryPolicy.ExecuteAsync(async () =>
        {
            var content = GetHttpContent(request);
            var response = await _httpClient.PostAsync(url, content);
            return await GetRequestContent<T>(response);
        });
    }

    private static async Task<Result<T>> GetRequestContent<T>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.Forbidden)
                return new Result<T>(true, response.StatusCode.ToString());
            response.EnsureSuccessStatusCode();
        }

        var json = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions);
        return data is null ? new Result<T>() : new Result<T>(data);
    }

    private static HttpContent GetHttpContent<T>(T content)
    {
        var json = JsonSerializer.Serialize(content, JsonSerializerOptions);
        return new StringContent(json, System.Text.Encoding.UTF8, "application/json");
    }

    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        serializerOptions.Converters.Add(new JsonStringEnumConverter());

        return serializerOptions;
    }
}