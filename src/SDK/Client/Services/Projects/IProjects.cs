using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Projects;

public interface IProjects
{
    Task<Result<Items<Project>>> ListAll();
}