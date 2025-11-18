using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.View.Pages.Main.Register;
using ProcessScaleGenerator.ViewModel.Pages.Main.Register;

namespace ProcessScaleGenerator
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var vm = MauiProgram.ServiceProvider.GetRequiredService<RegisterViewModel>();
            var window = new Window(new NavigationPage(new RegisterView(vm)));

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
    }
}