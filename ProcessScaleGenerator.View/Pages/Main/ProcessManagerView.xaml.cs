using ProcessScaleGenerator.ViewModel.Pages.Main.Processes;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class ProcessManagerView : ContentPage
{
	public ProcessManagerView(ProcessManagerViewModel processManagerViewModel)
	{
		InitializeComponent();
        BindingContext = processManagerViewModel;
    }
}