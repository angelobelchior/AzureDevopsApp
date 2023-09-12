using System;
using AzureDevops.Client.Models;
using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AzureDevops.ViewModels.Repos;

public class ReposViewModel : ObservableObject, IViewModel
{
    public ReposViewModel(IServiceLocator serviceLocator, Project project)
    {
    }

    public Task OnLoading() => Task.CompletedTask;
    public Task OnUnloading() => Task.CompletedTask;
}