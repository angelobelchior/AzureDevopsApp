using AzureDevops.Client;
using AzureDevops.Client.Models;
using AzureDevops.Client.Services;

namespace AzureDevops.Services;

public interface IServiceLocator
{
    IAzureDevopsClient AzureDevopsClient { get; }
    INavigationService Navigation { get; }
    IDialogService Dialog { get; }

    void RegisterAzureDevopsClient(string organization, string personalAccessToken);
}

internal class ServiceLocator : IServiceLocator
{
    private IAzureDevopsClient? _azureDevopsClient;
    public IAzureDevopsClient AzureDevopsClient => _azureDevopsClient ?? throw new ArgumentNullException();

    private INavigationService? _navigation;
    public INavigationService Navigation => _navigation ??= new NavigationService();

    private IDialogService? _dialog;
    public IDialogService Dialog => _dialog ??= new DialogService();

    private static IServiceLocator? _instance;
    public static IServiceLocator Instance => _instance ??= new ServiceLocator();

    public void RegisterAzureDevopsClient(string organization, string personalAccessToken)
        => _azureDevopsClient = AzureDevops.Client.AzureDevopsClient.Create(organization, personalAccessToken, RetryPolicyConfiguration.Default);
}