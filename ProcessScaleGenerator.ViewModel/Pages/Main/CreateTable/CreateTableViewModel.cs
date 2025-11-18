using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Employee;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;

public partial class CreateTableViewModel : ObservableObject, 
    IRecipient<EmployeesCountChanged>, IRecipient<ProcessesCountChanged>,
    IRecipient<TableGroupRemovedMessage>, IRecipient<TableGroupAddedMessage>,
    IRecipient<HiddedEmployeesCountChanged>, IRecipient<HiddedProcessesCountChanged>
{
    private readonly ToyotaEmployeeModel _toyotaEmployeeModel;
    private readonly ToyotaProcessModel _toyotaProcessModel;
    private readonly CreateTableModel _createTableModel;

    private readonly INavigationServices _navigationServices;
    private readonly IMessagingServices _messagingServices;
    private readonly IPopServices _popServices;
    private readonly IMessenger _messenger;

    public ObservableCollection<ToyotaTableGroup> Tables { get; set; } = [];

    public ObservableCollection<ToyotaProcessTable> Grup { get; set; } = [];

    public CreateTableViewModel(
        IMessenger messenger,
        IPopServices popServices ,
        IMessagingServices messagingServices, 
        INavigationServices navigationServices, 
        ToyotaEmployeeModel toyotaEmployeeModel, 
        ToyotaProcessModel toyotaProcessModel, 
        CreateTableModel createTableModel)
    {
        //Services
        _navigationServices = navigationServices;
        _messagingServices = messagingServices;
        _popServices = popServices;
        _messenger = messenger;

        //Models
        _toyotaEmployeeModel = toyotaEmployeeModel;
        _toyotaProcessModel = toyotaProcessModel;
        _createTableModel = createTableModel;
        
        _messenger.RegisterAll(this);
        InitialLoad();
    }

    private void InitialLoad()
    {
        Tables = [.._createTableModel.GetAllTables()];

        EmployeesCount = _toyotaEmployeeModel.GetAllEmployees().Count;
        ProcessesCount = _toyotaProcessModel.GetAllProcesses().Count;

        HiddenEmployeesCount = _toyotaEmployeeModel.GetAllEmployees().Where(E => E.Hidded).Count();
        HiddenProcessesCount = _toyotaProcessModel.GetAllProcesses().Where(P => P.Hidded).Count();
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

    [RelayCommand]
    public async Task SwitchToShowTables() => await _navigationServices.GoToPageAsync(RegisteredPages.ShowTable);
    [RelayCommand]
    public async Task SwitchToRegister() => await _navigationServices.GoToPageAsync(RegisteredPages.Register);
}
