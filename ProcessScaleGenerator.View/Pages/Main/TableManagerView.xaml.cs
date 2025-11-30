using ProcessScaleGenerator.ViewModel.Pages.Main.TableManager;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class TableManagerView : ContentView
{
	public TableManagerView(TableManagerViewModel tableManagerViewModel)
	{
		InitializeComponent();
		BindingContext = tableManagerViewModel;
	}

    private void OnUnloaded(object sender, EventArgs e)
    {
        if (BindingContext is TableManagerViewModel vm)
            vm.SendMessages();
    }
}