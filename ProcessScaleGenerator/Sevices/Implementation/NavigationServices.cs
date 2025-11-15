using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.View.Pages.Main.CreateTable;
using ProcessScaleGenerator.View.Pages.Main.Register;
using ProcessScaleGenerator.View.Pages.Main.ShowTable;
using ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;
using ProcessScaleGenerator.ViewModel.Pages.Main.Register;
using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

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
            case RegisteredPages.Register:
                var vmRegister = MauiProgram.ServiceProvider.GetRequiredService<RegisterViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new RegisterView(vmRegister));
                break;
            case RegisteredPages.CreateTable:
                var vmCreate = MauiProgram.ServiceProvider.GetRequiredService<CreateTableViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new CreateTableView(vmCreate));
                break;
            case RegisteredPages.ShowTable:
                var vmShow = MauiProgram.ServiceProvider.GetRequiredService<ShowTableViewModel>();
                await GetCurrentPage().Navigation.PushAsync(new ShowTableView(vmShow));
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
