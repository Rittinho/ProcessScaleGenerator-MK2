using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Settings
{
    public partial class SettingsViewModel
    {
        private readonly INavigationServices _navigationServices;
        public SettingsViewModel(INavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
        }
        [RelayCommand]
        public async Task SwitchToDashboard()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.Dashboard);
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
    }
}
