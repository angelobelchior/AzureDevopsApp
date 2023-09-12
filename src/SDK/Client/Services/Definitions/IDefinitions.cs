using System.Threading.Tasks;
using AzureDevops.Client.Models;

namespace AzureDevops.Client.Services.Definitions;

public interface IDefinitions
{
    Task<Result<Items<Definition>>> ListAll(string projectName);
}