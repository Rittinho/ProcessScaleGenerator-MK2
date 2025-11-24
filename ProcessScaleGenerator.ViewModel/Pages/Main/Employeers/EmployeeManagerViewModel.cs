using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Employee;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Employeers
{
    public partial class EmployeeManagerViewModel : ObservableObject, IRecipient<EmployeeAddedMessage>, IRecipient<EmployeeRemovedMessage>
    {
        private readonly IMessenger _messenger;
        private readonly IMessagingServices _messagingServices;
        private readonly INavigationServices _navigationServices;
        private readonly IPopServices _popServices;

        private readonly ToyotaEmployeeModel? _toyotaEmployeeModel;
        public enum RegisterMode { Create, Edit }
        public enum RegisterPanel { Process, Employee }

        [ObservableProperty]
        public bool? _isInEditMode;

        [ObservableProperty]
        public bool? _isInCreateMode;

        public EmployeeManagerViewModel(ToyotaEmployeeModel toyotaEmployeeModel, IMessenger messenger,
        IPopServices popServices, IMessagingServices messagingServices, INavigationServices navigationServices)
        {
            _toyotaEmployeeModel = toyotaEmployeeModel;
            _messagingServices = messagingServices;
            _navigationServices = navigationServices;
            _popServices = popServices;
            _messenger = messenger;

            SwitchMode(RegisterMode.Create);
            ClearEmployeeFilds();
            _messenger.RegisterAll(this);
            LoadInitialData();
        }
        private void LoadInitialData()
        {
            var initialEmployees = _toyotaEmployeeModel.GetAllEmployees();

            EmployeeList.Clear();
            FiltredEmployeeList.Clear();

            foreach (var employee in initialEmployees)
            {
                EmployeeList.Add(employee);
                FiltredEmployeeList.Add(employee);
            }
        }
        public void SwitchMode(RegisterMode mode)
        {
            switch (mode)
            {
                case RegisterMode.Create:
                    IsInCreateMode = true;
                    IsInEditMode = false;
                    break;
                case RegisterMode.Edit:
                    IsInCreateMode = false;
                    IsInEditMode = true;
                    break;
                default:
                    break;
            }
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
