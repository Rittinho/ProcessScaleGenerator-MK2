using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly INavigationServices _navigationServices;

        [ObservableProperty]
        private object _currentView;

        [ObservableProperty]
        public bool? _firstWindow;

        public MainPageViewModel(INavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
            SwitchToDashboard();

            FirstWindow = true;
        }

        [RelayCommand]
        public async Task SwitchToDashboard()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.Dashboard);
        }
        [RelayCommand]
        public async Task SwitchToTableManager()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.TableManager);
        }
        [RelayCommand]
        public async Task SwitchToProcessesManager()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.Processes);
        }
        [RelayCommand]
        public async Task SwitchToEmployeersManager()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.Employeers);
        }
        [RelayCommand]
        public async Task SwitchToShowTables()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.ShowTable);
        }
        [RelayCommand]
        public async Task SwitchToSettings()
        {
            FirstWindow = false;
            CurrentView = _navigationServices.GetViewAsync(RegisteredPages.Settings);
        }
    }
}
