using AzureDevops.Client.Models;
using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AzureDevops.ViewModels;

public partial class ProjectsPageViewModel: ObservableObject, IViewModel
{
    private readonly IServiceLocator _serviceLocator;
    public IEnumerable<Project> Projects { get; set; } 

    public ProjectsPageViewModel(IServiceLocator serviceLocator, IEnumerable<Project> projects)
    {
        _serviceLocator = serviceLocator;
        
        Projects = projects;
    }
    
    public Task OnLoading() => Task.CompletedTask;
    public Task OnUnloading() => Task.CompletedTask;
    
    [RelayCommand]
    private async Task LoadProjectFeatures(Project project)
        => await _serviceLocator.Navigation.ShowPipeline(project);
}