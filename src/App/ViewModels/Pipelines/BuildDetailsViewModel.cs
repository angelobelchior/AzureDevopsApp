using CommunityToolkit.Mvvm.ComponentModel;
using AzureDevops.Services;
using System.Collections.ObjectModel;
using AzureDevops.Client.Models;
using AzureDevops.Models;
using CommunityToolkit.Mvvm.Input;

namespace AzureDevops.ViewModels.Pipelines;

public partial class BuildDetailsViewModel : ObservableObject, IViewModel
{
    public ObservableCollection<Job> Jobs { get; set; }
    public Project Project { get; set; }
    public Build Build { get; set; } 

    private readonly IServiceLocator _serviceLocator;

    public BuildDetailsViewModel(IServiceLocator serviceLocator, Project project, Build build)
	{
        _serviceLocator = serviceLocator;

        Project = project;
        Build = build;
        Jobs = new();
    }

    public async Task OnLoading() => await LoadTimelineCommand.ExecuteAsync(null);
    public Task OnUnloading() => Task.CompletedTask;

    [RelayCommand]
    private async Task LoadTimeline()
    {
        var result = await _serviceLocator.AzureDevopsClient.Builds.GetTimeline(Project.Name, Build.Id);
        if (result.HasError)
            await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");

        if (result.Data == null) return;
        
        Jobs.Clear();
        var jobs = Job.CreateJobs(result.Data.Records);
        foreach (var job in jobs)
            Jobs.Add(job);
    }

    [RelayCommand]
    private async Task ShowLogs(Job job)
    {
        if (job.LogId.HasValue)
        {
            var result = await _serviceLocator.AzureDevopsClient.Builds.GetLogs(Project.Name, Build.Id, job.LogId.Value);
            if (result.HasError)
                await _serviceLocator.Dialog.ShowToast($"Error... {result.ErrorDescription}");

            if (result.Data is null) return;
            await _serviceLocator.Navigation.ShowBuildLog(result.Data.Value);
        }
    }
}