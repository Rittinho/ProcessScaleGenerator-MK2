using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using ProcessScaleGenerator.Model.Employee;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Sevices.Implementation;
using ProcessScaleGenerator.Sevices.Injections.Implementation;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Injections.Implementation;
using ProcessScaleGenerator.Shared.Injections.Implementation.Repository;
using ProcessScaleGenerator.View.Pages.Main.CreateTable;
using ProcessScaleGenerator.View.Pages.Main.Register;
using ProcessScaleGenerator.View.Pages.Main.ShowTable;
using ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;
using ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;
using ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;
using ProcessScaleGenerator.ViewModel.Pages.Main.Register;
using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;
using ToyotaProcessManager.Services.Injections.Contract;
using ToyotaProcessManager.Services.Injections.Implementation;

namespace ProcessScaleGenerator
{
    public static class MauiProgram
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("ToyotaFont.otf", "TOYOTA");

                    fonts.AddFont("FAR.otf", "FAR");
                    fonts.AddFont("FAS.otf", "FAS");
                    fonts.AddFont("FAB.otf", "FAB");
                });

#if DEBUG
            builder.Logging.AddDebug();

            builder.Services.AddSingleton<IMessagingServices, MessagingServices>();
            builder.Services.AddSingleton<INavigationServices, NavigationServices>();
            builder.Services.AddTransient<IPopServices, PopServices>();

            builder.Services.AddSingleton<IRepositoryServices, RepositoryServices>();
            builder.Services.AddSingleton<IJsonServices, JsonServices>();
            builder.Services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            //views
            builder.Services.AddTransient<CreateTableView>();
            builder.Services.AddTransient<ShowTableView>();
            builder.Services.AddTransient<RegisterView>();

            //ViewModels
            builder.Services.AddSingleton<CreateTableViewModel>();
            builder.Services.AddSingleton<ShowTableViewModel>();
            builder.Services.AddSingleton<RegisterViewModel>();

            builder.Services.AddTransient<TableConfigModalViewModel>();
            builder.Services.AddTransient<IconPickerModalViewModel>();

            //models
            builder.Services.AddTransient<ToyotaEmployeeModel>();
            builder.Services.AddTransient<ToyotaProcessModel>();
            builder.Services.AddTransient<CreateTableModel>();

            var app = builder.Build();

            ServiceProvider = app.Services;
#endif

            return app;
        }
    }
}
