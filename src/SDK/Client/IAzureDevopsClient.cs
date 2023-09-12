using AzureDevops.Client.Services;

namespace AzureDevops.Client;

public interface IAzureDevopsClient
{
    Services.Projects.IProjects Projects { get; }
    Services.Builds.IBuilds Builds { get; }
    Services.Definitions.IDefinitions Definitions { get; }
}