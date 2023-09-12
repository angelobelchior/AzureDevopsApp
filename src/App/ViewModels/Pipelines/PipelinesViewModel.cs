using System;
using System.Collections.ObjectModel;
using AzureDevops.Client.Models;
using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AzureDevops.ViewModels.Pipelines;

public partial class PipelinesViewModel : ObservableObject, IViewModel
{
    public ObservableCollection<Definition> Definitions { get; set; }
    public ObservableCollection<Build> Builds { get; set; }

    private readonly IServiceLocator _serviceLocator;
    private readonly Project _project;
    public PipelinesViewModel(IServiceLocator serviceLocator, Project project)
	{
        _serviceLocator = serviceLocator;
        _project = project;

        Definitions = new ObservableCollection<Definition>();
        Builds = new ObservableCollection<Build>();
    }

    public async Task OnLoading() => await LoadDefinitions();

    public Task OnUnloading() => Task.CompletedTask;

    [RelayCommand]
    private async Task RefreshBuilds(Definition definition)
            => await LoadBuilds(definition);

    [RelayCommand]
    private async Task QueueBuild(Definition? definition)
    {
        if (definition is null) return;

        var result = await _serviceLocator.AzureDevopsClient.Builds.Queue(definition);
        if (result.HasError)
            await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");
        else if (result?.Data is not null)
            Builds.Insert(0, result.Data);
    }

    [RelayCommand]
    private async Task ShowBuildDetails(Build build)
        => await _serviceLocator.Navigation.ShowBuildDetails(_project, build);

    private async Task LoadDefinitions()
    {
        var result = await _serviceLocator.AzureDevopsClient.Definitions.ListAll(_project.Name);
        if (result.HasError)
            await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");
        else
        {
            var definitions = result.Data?.Value?.OrderByDescending(d => d.Id);
            if (definitions is null) return;

            foreach (var definition in definitions) Definitions.Add(definition);
        }
    }

    private async Task LoadBuilds(Definition definition)
    {
        var result = await _serviceLocator.AzureDevopsClient.Builds.ListAll(_project.Name, definition.Id);
        if (result.HasError)
            await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");
        else
        {
            if (result.Data is null) return;

            Builds.Clear();
            foreach (var build in result.Data.Value)
                Builds.Add(build);
        }
    }
}