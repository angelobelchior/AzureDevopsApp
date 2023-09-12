using AzureDevops.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AzureDevops.ViewModels.Pipelines;

public partial class BuildLogViewModel : ObservableObject, IViewModel
{
    public IEnumerable<string> Logs { get; set; }
    public BuildLogViewModel(IEnumerable<string> logs)
    {
        Logs = logs;
    }
    
    public Task OnLoading() => Task.CompletedTask;
    public Task OnUnloading()=> Task.CompletedTask;
}