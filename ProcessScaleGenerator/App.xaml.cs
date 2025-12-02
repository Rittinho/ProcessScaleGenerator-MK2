using ProcessScaleGenerator.Resources.Styles;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Injections.Implementation;
using ProcessScaleGenerator.View.Pages;
using ProcessScaleGenerator.View.Pages.Main;
using ProcessScaleGenerator.ViewModel.Pages.Main;
using ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard;

namespace ProcessScaleGenerator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var appSettings = MauiProgram.ServiceProvider.GetRequiredService<IAppSettings>();
            LoadThemeColors(appSettings.CurrentTheme());
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var vm = MauiProgram.ServiceProvider.GetRequiredService<MainPageViewModel>();
            var window = new Window(new NavigationPage(new MainPage(vm)));

            window.Destroying += OnWindowDestroying;

            window.Height = 720;
            window.MinimumHeight = 720;
            window.Width = 1280;
            window.MinimumWidth = 1280;
            return window;
        }
        private void OnWindowDestroying(object? sender, EventArgs e)
        {
            var repository = MauiProgram.ServiceProvider.GetService<IRepositoryServices>();

            if (repository is not null)
            {
                repository.SaveAllData();
            }
        }
        public void LoadThemeColors(string theme)
        {
            // Primeiro, limpamos qualquer listener antigo para não duplicar
            Application.Current.RequestedThemeChanged -= OnSystemThemeChanged;

            switch (theme)
            {
                case "Dark":
                    UpdateManualTheme(AppTheme.Dark);
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    break;

                case "Light":
                    UpdateManualTheme(AppTheme.Light);
                    Application.Current.UserAppTheme = AppTheme.Light;
                    break;

                case "System":
                default:
                    // 1. Aplica o estado ATUAL do sistema
                    UpdateManualTheme(AppInfo.RequestedTheme);
                    Application.Current.UserAppTheme = AppTheme.Unspecified;
                    // 2. IMPORTANTE: Começa a vigiar o sistema
                    // Se o sol se pôr e o Windows virar Dark, o app acompanha
                    Application.Current.RequestedThemeChanged += OnSystemThemeChanged;
                    break;
            }
        }
        private void OnSystemThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            // Repassa a mudança do sistema para nossa troca de dicionários
            UpdateManualTheme(e.RequestedTheme);
        }

        private void UpdateManualTheme(AppTheme theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;

            if (mergedDictionaries != null)
            {
                var temaAntigo = mergedDictionaries.FirstOrDefault(d => d is LightColors || d is DarkColors);

                var estiloAntigo = mergedDictionaries.FirstOrDefault(d => d is GlobalStyles);

                // Remove apenas se encontrou (não dá erro se for a primeira vez)
                if (temaAntigo != null)
                    mergedDictionaries.Remove(temaAntigo);

                if (estiloAntigo != null)
                    mergedDictionaries.Remove(estiloAntigo);

                ResourceDictionary novoTema = theme == AppTheme.Dark ? new DarkColors() : new LightColors();

                mergedDictionaries.Add(novoTema);

                mergedDictionaries.Add(new GlobalStyles());
            }
        }
    }
}