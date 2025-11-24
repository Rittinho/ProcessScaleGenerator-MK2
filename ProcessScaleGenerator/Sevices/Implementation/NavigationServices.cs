using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.View.Pages.Main;
using ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard;
using ProcessScaleGenerator.ViewModel.Pages.Main.Employeers;
using ProcessScaleGenerator.ViewModel.Pages.Main.Processes;
using ProcessScaleGenerator.ViewModel.Pages.Main.Settings;
using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;
using ProcessScaleGenerator.ViewModel.Pages.Main.TableManager;

namespace ProcessScaleGenerator.Sevices.Injections.Implementation;

public class NavigationServices : INavigationServices
{
    public Task GoToBackAsync()
    {
        throw new NotImplementedException();
    }

    public Task GoToHomeAsync()
    {
        throw new NotImplementedException();
    }

    public async Task GoToPageAsync(RegisteredPages page)
    {
        switch (page)
        {
            case RegisteredPages.Dashboard:
                var vmDashboard = MauiProgram.ServiceProvider.GetRequiredService<DashboardViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new DashboardView(vmDashboard));
                break;
            case RegisteredPages.Processes:
                var vmProcess = MauiProgram.ServiceProvider.GetRequiredService<ProcessManagerViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new ProcessManagerView(vmProcess));
                break;
            case RegisteredPages.Employeers:
                var vmEmployee = MauiProgram.ServiceProvider.GetRequiredService<EmployeeManagerViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new EmployeeManagerView(vmEmployee));
                break;
            case RegisteredPages.TableManager:
                var vmTableManager = MauiProgram.ServiceProvider.GetRequiredService<TableManagerViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new TableManagerView(vmTableManager));
                break;
            case RegisteredPages.ShowTable:
                var vmShow = MauiProgram.ServiceProvider.GetRequiredService<ShowTableViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new ShowTableView(vmShow));
                break;
            case RegisteredPages.Settings:
                var vmSettings = MauiProgram.ServiceProvider.GetRequiredService<SettingsViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new SettingsView(vmSettings));
                break;
        }
    }
    private Page? GetCurrentPage()
    {
        if (Shell.Current?.CurrentPage != null)
            return Shell.Current.CurrentPage;

        var mainWindow = Application.Current?.Windows.FirstOrDefault();
        return mainWindow?.Page;
    }
}
