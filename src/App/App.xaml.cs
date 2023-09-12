using AzureDevops.Services;

namespace AzureDevops;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        ServiceLocator.Instance.Navigation.ShowPersonalAccessTokenLoginPage();
    }
}