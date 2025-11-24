using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class ShowTableView : ContentPage
{
	public ShowTableView(ShowTableViewModel showTableViewModel)
	{
		InitializeComponent();
		BindingContext = showTableViewModel;
	}
}