using AzureDevops.ViewModels.Pipelines;

namespace AzureDevops.Views.Pipelines;

public partial class BuildDetailsPage : ContentPage
{
	private BuildDetailsViewModel ViewModel => (BuildDetailsViewModel)BindingContext;
	
	public BuildDetailsPage()
	{
		InitializeComponent();
		
		Jobs.ItemTapped += (sender, e)
			=> ViewModel.ShowLogsCommand.Execute(e.Item as Models.Job);
	}
}
