using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard
{
    public partial class DashboardViewModel : ObservableObject, IRecipient<EmployeesCountChanged>, IRecipient<ProcessesCountChanged>, IRecipient<TableGroupRemovedMessage>, IRecipient<TableGroupAddedMessage>, IRecipient<HiddedEmployeesCountChanged>, IRecipient<HiddedProcessesCountChanged>
    {
        private readonly CreateTableModel _createTableModel;

        private readonly IMessagingServices _messagingServices;
        private readonly IRepositoryServices _repositoryServices;
        private readonly IPopServices _popServices;
        private readonly IMessenger _messenger;

        public ObservableCollection<ToyotaTableGroup> Tables { get; set; } = [];

        public DashboardViewModel(IMessenger messenger, IRepositoryServices repositoryServices, IPopServices popServices, IMessagingServices messagingServices, CreateTableModel createTableModel)
        {
            _createTableModel = createTableModel;

            _repositoryServices = repositoryServices;
            _messagingServices = messagingServices;
            _popServices = popServices;
            _messenger = messenger;

            _messenger.RegisterAll(this);

            InitialLoad();
        }

        private void InitialLoad()
        {
            Tables = [.. _createTableModel.GetAllTables()];

            EmployeesCount = _repositoryServices.GetAllEmployees().Count;
            ProcessesCount = _repositoryServices.GetAllProcesses().Count;

            HiddenEmployeesCount = _repositoryServices.GetAllEmployees().Where(E => E.Hidded).Count();
            HiddenProcessesCount = _repositoryServices.GetAllProcesses().Where(P => P.Hidded).Count();
        }
        [RelayCommand]
        public async Task ShowTableGroup(ToyotaTableGroup toyotaTableGroup)
        {
            if (toyotaTableGroup is null)
            {
                await _popServices.WaringPopup(WarningTokens.CorruptFile);
                return;
            }

            await _popServices.ShowTableGroupPopup(toyotaTableGroup);
        }
        [RelayCommand]
        public async Task DeleteTableGroup(ToyotaTableGroup toyotaTableGroup)
        {
            if (!await _popServices.ConfirmPopup(WarningTokens.DeleteProcess))
                return;

            if (_createTableModel.DeleteTable(toyotaTableGroup))
                await _popServices.WaringPopup(WarningTokens.DeleteSuccess);
        }
    }
}
