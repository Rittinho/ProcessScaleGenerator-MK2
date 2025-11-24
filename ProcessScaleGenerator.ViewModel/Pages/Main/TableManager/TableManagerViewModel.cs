using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.TableManager
{
    public partial class TableManagerViewModel : ObservableObject
    {
        private readonly INavigationServices _navigationServices;
        private readonly IRepositoryServices _repositoryServices;
        private readonly IMessenger _messenger;

        public TableManagerViewModel(INavigationServices navigationServices, IRepositoryServices repositoryServices, IMessenger messenger)
        {
            _navigationServices = navigationServices;
            _repositoryServices = repositoryServices;
            _messenger = messenger;

            ProcessList = _repositoryServices.GetAllProcesses();
            EmployeeList = _repositoryServices.GetAllEmployees();

            FiltredProcessList = [.. ProcessList];
            FiltredEmployeeList = [.. EmployeeList];
        }


        [RelayCommand]
        public async Task SwitchToDashboard()
        {
            _navigationServices.GoToPageAsync(RegisteredPages.Dashboard);
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
