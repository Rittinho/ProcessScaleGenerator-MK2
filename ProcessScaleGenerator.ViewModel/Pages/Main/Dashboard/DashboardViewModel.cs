using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard
{

    public partial class DashboardViewModel
    {
        private readonly INavigationServices _navigationServices;
        public DashboardViewModel(INavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
        }
        [RelayCommand]
        public async Task SwitchToTableManager()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.TableManager);
        }
        [RelayCommand]
        public async Task SwitchToProcessesManager()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.Processes);
        }
        [RelayCommand]
        public async Task SwitchToEmployeersManager()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.Employeers);
        }
        [RelayCommand]
        public async Task SwitchToShowTables()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.ShowTable);
        }
        [RelayCommand]
        public async Task SwitchToSettings()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.Settings);
        }
    }
}
