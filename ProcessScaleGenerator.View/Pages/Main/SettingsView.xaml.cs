using ProcessScaleGenerator.ViewModel.Pages.Main.Settings;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class SettingsView : ContentView
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}
}