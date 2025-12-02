using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;

public partial class CreateTableViewModel : ObservableObject, IRecipient<EmployeesCountChanged>, IRecipient<ProcessesCountChanged>, IRecipient<TableGroupRemovedMessage>, IRecipient<TableGroupAddedMessage>, IRecipient<HiddedEmployeesCountChanged>, IRecipient<HiddedProcessesCountChanged>
{
    private readonly CreateTableModel _createTableModel;

    private readonly IMessagingServices _messagingServices;
    private readonly IRepositoryServices _repositoryServices;
    private readonly IPopServices _popServices;
    private readonly IMessenger _messenger;

    public ObservableCollection<ToyotaTableGroup> Tables { get; set; } = [];
    public ObservableCollection<ToyotaProcessTable> Grup { get; set; } = [];

    public CreateTableViewModel(IMessenger messenger, IRepositoryServices repositoryServices, IPopServices popServices, IMessagingServices messagingServices, CreateTableModel createTableModel)
    {
        _messagingServices = messagingServices;
        _popServices = popServices;
        _messenger = messenger;

        _createTableModel = createTableModel;

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
}
