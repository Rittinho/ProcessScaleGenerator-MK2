using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator;
using ProcessScaleGenerator.ViewModel.Pages.Main.Settings;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class SettingsView : ContentView
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;

    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        if (BindingContext is SettingsViewModel vm)
            vm.SaveSettings();
    }

    [RelayCommand]

    private async void Linkedin()
    {
        try
        {
            Uri uri = new Uri("https://www.linkedin.com/in/israel-mendes-71651b34b/");
            BrowserLaunchOptions options = new BrowserLaunchOptions()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
            };

            await Browser.Default.OpenAsync(uri, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir site: {ex.Message}");
        }
    }

    [RelayCommand]
    private async void Github()
    {
        try
        {
            Uri uri = new Uri("https://github.com/Rittinho");
            BrowserLaunchOptions options = new BrowserLaunchOptions()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
            };

            await Browser.Default.OpenAsync(uri, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir site: {ex.Message}");
        }
    }

}