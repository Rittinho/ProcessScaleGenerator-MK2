using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.ViewModel.Pages.Main.Settings;
using ProcessScaleGenerator.ViewModel.Pages.Main.TableManager;

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
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Colors.Violet, // Opcional: Personaliza a cor da barra
                PreferredControlColor = Colors.White   // Opcional: Cor dos botões
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
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Colors.Violet, // Opcional: Personaliza a cor da barra
                PreferredControlColor = Colors.White   // Opcional: Cor dos botões
            };

            await Browser.Default.OpenAsync(uri, options);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir site: {ex.Message}");
        }
    }
}