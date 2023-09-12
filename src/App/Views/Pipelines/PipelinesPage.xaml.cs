using AzureDevops.ViewModels.Pipelines;


namespace AzureDevops.Views.Pipelines;

public partial class PipelinesPage : ContentPage
{
    private PipelinesViewModel ViewModel => (PipelinesViewModel)BindingContext;

    public PipelinesPage()
	{
		InitializeComponent();

        Definitions.SelectedIndexChanged += (sender, e) =>
        {
            ViewModel.RefreshBuildsCommand.Execute(Definitions.SelectedItem);
        };
    }
}
