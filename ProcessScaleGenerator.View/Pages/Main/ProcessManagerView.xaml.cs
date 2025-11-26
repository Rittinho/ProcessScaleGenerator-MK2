using ProcessScaleGenerator.ViewModel.Pages.Main.Processes;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class ProcessManagerView : ContentView
{
	public ProcessManagerView(ProcessManagerViewModel processManagerViewModel)
	{
		InitializeComponent();
        BindingContext = processManagerViewModel;
    }
}