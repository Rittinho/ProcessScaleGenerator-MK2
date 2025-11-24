using ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class DashboardView : ContentPage
{
	public DashboardView(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
		BindingContext = dashboardViewModel;
	}
}