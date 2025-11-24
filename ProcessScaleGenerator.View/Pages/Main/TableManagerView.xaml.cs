using ProcessScaleGenerator.ViewModel.Pages.Main.TableManager;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class TableManagerView : ContentPage
{
	public TableManagerView(TableManagerViewModel tableManagerViewModel)
	{
		InitializeComponent();
		BindingContext = tableManagerViewModel;
	}
}