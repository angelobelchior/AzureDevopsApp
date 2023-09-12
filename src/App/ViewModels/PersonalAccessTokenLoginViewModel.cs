using AzureDevops.Client.Models;
using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AzureDevops.ViewModels;

public partial class PersonalAccessTokenLoginViewModel : ObservableObject, IViewModel
{
    [ObservableProperty] private string _organization = string.Empty;
    [ObservableProperty] private string _personalAccessToken= string.Empty;

    private readonly IServiceLocator _serviceLocator;

    public PersonalAccessTokenLoginViewModel(IServiceLocator serviceLocator)
    {
        _serviceLocator = serviceLocator;
    }

    private bool CanExecuteLogin()
        => !string.IsNullOrEmpty(Organization) &&
           !string.IsNullOrEmpty(PersonalAccessToken);

    [RelayCommand]
    private async Task OpenUrl()
        => await _serviceLocator.Navigation.OpenBrowser(Constants.URL_PERSONAL_ACCESS_TOKEN);

    [RelayCommand(CanExecute = nameof(CanExecuteLogin))]
    private async Task Login()
    {
        _serviceLocator.RegisterAzureDevopsClient(Organization, PersonalAccessToken);
        var result = await _serviceLocator.AzureDevopsClient.Projects.ListAll();
        if (result.HasError)
            await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");
        else
        {
            var projects = result.Data?.Value.OrderByDescending(p => p.LastUpdateTime);
            _serviceLocator.Navigation.ShowProjectsPage(projects ?? Enumerable.Empty<Project>());
        }
    }

    public Task OnLoading() => Task.CompletedTask;
    public Task OnUnloading() => Task.CompletedTask;
}