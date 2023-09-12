using System;
using AzureDevops.Client.Models;
using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AzureDevops.ViewModels.Pipelines;

public class TestViewModel : ObservableObject, IViewModel
{
    public TestViewModel(IServiceLocator serviceLocator, Project project)
    {
    }

    public Task OnLoading() => Task.CompletedTask;
    public Task OnUnloading() => Task.CompletedTask;
}