using AzureDevops.Client.Models;
using AzureDevops.ViewModels;
using AzureDevops.ViewModels.Pipelines;
using AzureDevops.ViewModels.Repos;
using AzureDevops.Views;
using AzureDevops.Views.Pipelines;
using AzureDevops.Views.Repos;

namespace AzureDevops.Services;

public interface INavigationService
{
    void ShowPersonalAccessTokenLoginPage();
    void ShowProjectsPage(IEnumerable<Project> projects);

    Task ShowPipeline(Project project);
    Task ShowBuildDetails(Project project, Build build);
    Task ShowBuildLog(IEnumerable<string> logs);
    Task OpenBrowser(string url);
}

internal class NavigationService : INavigationService
{
    public void ShowPersonalAccessTokenLoginPage()
    {
        var page = BindPage(new PersonalAccessTokenLoginPage(),
            new PersonalAccessTokenLoginViewModel(ServiceLocator.Instance));

        Application.Current!.MainPage = new NavigationPage(page);
    }

    public void ShowProjectsPage(IEnumerable<Project> projects)
    {
        Application.Current!.MainPage = new FlyoutPage
        {
            Flyout = BindPage(new ProjectsPage(), new ProjectsPageViewModel(ServiceLocator.Instance, projects)),
            Detail = new NavigationPage(new EmptyPage())
        };
    }

    public async Task ShowPipeline(Project project)
    {
        if (Application.Current!.MainPage is not FlyoutPage flyoutPage) return;

        var tabbedPage = new TabbedPage
        {
            Title = project.Name,
            Children =
                {
                    BindPage(new PipelinesPage(), new PipelinesViewModel(ServiceLocator.Instance, project)),
                    //BindPage(new ReposPage(), new ReposViewModel(ServiceLocator.Instance, project)),
                    //BindPage(new TestPage(), new TestViewModel(ServiceLocator.Instance, project)),
                }
        };

        flyoutPage.IsPresented = false;
        await flyoutPage.Detail.Navigation.PushAsync(tabbedPage);
    }

    public async Task ShowBuildDetails(Project project, Build build)
    {
        if (Application.Current!.MainPage is FlyoutPage flyoutPage)
        {
            var buildDetails = BindPage(new BuildDetailsPage(),
                                        new BuildDetailsViewModel(ServiceLocator.Instance, project, build));
            await flyoutPage.Detail.Navigation.PushAsync(buildDetails);
        }
    }

    public async Task ShowBuildLog(IEnumerable<string> logs)
    {
        var buildLogPage = BindPage(new BuildLogPage(), new BuildLogViewModel(logs));
        await Application.Current!.MainPage!.Navigation.PushModalAsync(buildLogPage);
    }

    public Task OpenBrowser(string url)
        => Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);


    private static Page BindPage(Page page, IViewModel viewModel)
    {
        page.BindingContext = viewModel;
        page.Appearing += async (sender, e) => await viewModel.OnLoading();
        page.Disappearing += async (sender, e) => await viewModel.OnUnloading();
        return page;
    }
}