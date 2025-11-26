using ProcessScaleGenerator.Resources.Styles;
using ProcessScaleGenerator.Shared.Injections.Contract;
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
            LoadThemeColors(AppInfo.RequestedTheme);
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
            //return new Window(new AppShell());
        }
        private void OnWindowDestroying(object? sender, EventArgs e)
        {
            var repository = MauiProgram.ServiceProvider.GetService<IRepositoryServices>();

            if (repository is not null)
            {
                repository.SaveAllData();
            }
        }
        private void LoadThemeColors(AppTheme theme)
        {
            var styles = Application.Current.Resources.MergedDictionaries;

            var themeStyle = new ResourceDictionary();

            switch (theme)
            {
                case AppTheme.Dark:
                    styles.Add(new DarkColors());
                    break;
                case AppTheme.Light:
                    styles.Add(new LightColors());
                    break;
                case AppTheme.Unspecified:
                    break;
            }

            styles.Add(themeStyle);
        }
    }
}