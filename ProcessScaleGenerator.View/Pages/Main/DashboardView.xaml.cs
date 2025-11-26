using ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class DashboardView : ContentView
{
	public DashboardView(DashboardViewModel dashboardViewModel)
	{
		InitializeComponent();
		BindingContext = dashboardViewModel;
	}
}