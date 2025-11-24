using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Processes
{
    public partial class ProcessManagerViewModel : ObservableObject, IRecipient<ProcessAddedMessage>, IRecipient<ProcessRemovedMessage>
    {
        private readonly IMessenger _messenger;
        private readonly IMessagingServices _messagingServices;
        private readonly INavigationServices _navigationServices;
        private readonly IPopServices _popServices;

        private readonly ToyotaProcessModel? _toyotaProcessModel;

        public enum RegisterMode { Create, Edit }
        public enum RegisterPanel { Process, Employee }

        [ObservableProperty]
        public bool? _isInEditMode;

        [ObservableProperty]
        public bool? _isInCreateMode;


        public ProcessManagerViewModel(ToyotaProcessModel toyotaProcessModel, IMessenger messenger,
        IPopServices popServices, IMessagingServices messagingServices, INavigationServices navigationServices)
        {
            _toyotaProcessModel = toyotaProcessModel;
            _messagingServices = messagingServices;
            _navigationServices = navigationServices;
            _popServices = popServices;
            _messenger = messenger;

            SwitchMode(RegisterMode.Create);
            ClearProcessFilds();
            _messenger.RegisterAll(this);
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            var initialProcesses = _toyotaProcessModel.GetAllProcesses();

            ProcessList.Clear();
            FiltredProcessList.Clear();

            foreach (var process in initialProcesses)
            {
                ProcessList.Add(process);
                FiltredProcessList.Add(process);
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
