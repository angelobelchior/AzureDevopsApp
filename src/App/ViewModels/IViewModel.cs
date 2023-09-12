namespace AzureDevops.ViewModels;

public interface IViewModel
{
    Task OnLoading();
    Task OnUnloading();
}