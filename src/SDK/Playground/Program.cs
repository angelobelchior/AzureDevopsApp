using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.TestManagement.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using AzureDevops.Client.Models;
using AzureDevops.Client.Services;

namespace AzureDevops.Client.Playground;

public static class Program
{
    private const string ORGANIZATION = "angelobelchior";

    private const string TOKEN = "t7pjvtn5rrya4qk62ychcgpfjkgkn2vnlcqxseue63kskkuor56q";

    //private const string PROJECT_NAME = "SmartHotel360";
    private const string PROJECT_NAME = "ESX Hackathon";

    //public static async Task Main()
    //{
    //    var uri = new Uri($"https://dev.azure.com/{ORGANIZATION}");
    //    var credentials = new VssBasicCredential(string.Empty, TOKEN);
    //    var connection = new VssConnection(uri, credentials);

    //    await Projects(connection);
    //}

    //private static async Task Projects(VssConnection connection)
    //{
    //    var client = await connection.GetClientAsync<ProjectHttpClient>();
    //    var projects = await client.GetProjects(ProjectState.All);
    //    foreach (var project in projects)
    //    {
    //        Console.WriteLine($"Project {project.Name}");
    //        await Definitions(connection, project.Name);
    //        Console.WriteLine("===================================================");
    //    }
    //}

    //private static async Task Definitions(VssConnection connection, string projectName)
    //{
    //    var client = await connection.GetClientAsync<BuildHttpClient>();
    //    var definitions = await client.GetDefinitionsAsync(projectName);
    //    foreach (var definition in definitions)
    //    {
    //        Console.WriteLine($" - Definition {definition.Name}");
    //        await Builds(client, projectName);
    //    }
    //}

    //private static async Task Builds(BuildHttpClient client, string projectName)
    //{
    //    var builds = await client.GetBuildsAsync(projectName);
    //    foreach (var build in builds)
    //    {
    //        Console.WriteLine($"  - Build {build.BuildNumber}");
    //        //await Logs(client, projectName, build.Id);
    //        Console.WriteLine("---");
    //        await Changes(client, projectName, build.Id);
    //    }
    //}

    //private static async Task Logs(BuildHttpClient client, string projectName, int buildId)
    //{
    //    var logs = await client.GetBuildLogsAsync(projectName, buildId);
    //    foreach (var log in logs)
    //    {
    //        var lines = await client.GetBuildLogLinesAsync(projectName, buildId, log.Id);
    //        foreach (var line in lines)
    //            Console.WriteLine($"   - LINE: {line}");
    //    }
    //}

    //private static async Task Changes(BuildHttpClient client, string projectName, int buildId)
    //{
    //    var changes = await client.GetBuildChangesAsync(projectName, buildId);
    //    foreach (var change in changes)
    //    {
    //        Console.WriteLine($"   - Change {change.Author.DisplayName} {change.Message}");
    //    }
    //}

    //private static async Task Timeline()
    //{

    //}

    public static async Task Main()
    {
        var client = AzureDevopsClient.Create(ORGANIZATION, TOKEN, RetryPolicyConfiguration.Default);
        var projects = await client.Projects.ListAll();
        var project = projects.Data!.Value.FirstOrDefault(p => p.Name.Equals(PROJECT_NAME));
        var definitions = await client.Definitions.ListAll(PROJECT_NAME);
        var definition = definitions.Data!.Value.First();
        var build = await client.Builds.Queue(definition);
        var builds = await client.Builds.ListAll(PROJECT_NAME, definition.Id);

        var buildId = builds.Data!.Value.Last().Id;
        var changes = await client.Builds.ListChanges(PROJECT_NAME, buildId);
        var timeline = await client.Builds.GetTimeline(PROJECT_NAME, buildId);

        foreach (var record in timeline.Data!.Records.OrderBy(r => r.ParentId).ThenBy(r => r.Id))
        {
            Console.WriteLine($"{record.Id} - {record.ParentId} - {record.Name}");
            var log = await client.Builds.GetLogs(PROJECT_NAME, buildId, record.Log.Id);
            Console.WriteLine(log);
        }

        Console.WriteLine("======");
    }
}