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
using static ProcessScaleGenerator.ViewModel.Pages.Main.Register.RegisterViewModel;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;

public partial class CreateTableViewModel : ObservableObject, IRecipient<EmployeesCountChanged>, IRecipient<ProcessesCountChanged>,
     IRecipient<HiddedEmployeesCountChanged>, IRecipient<HiddedProcessesCountChanged>, IRecipient<TableGroupRemovedMessage>, IRecipient<TableGroupAddedMessage>
{
    private readonly IMessenger _messenger;
    private readonly IMessagingServices _messagingServices;
    private readonly INavigationServices _navigationServices;
    private readonly IPopServices _popServices;

    private readonly ToyotaEmployeeModel _toyotaEmployeeModel;
    private readonly ToyotaProcessModel _toyotaProcessModel;

    [ObservableProperty]
    private int? _hiddenProcessesCount;

    [ObservableProperty]
    private int? _hiddenEmployeesCount;

    [ObservableProperty]
    private int? _processesCount;

    [ObservableProperty]
    private int? _employeesCount;

    private readonly CreateTableModel _createTableModel;
    public ObservableCollection<ToyotaTableGroup> Tables { get; set; } = [];

    public ObservableCollection<ToyotaProcessTable> Grup { get; set; }

    public CreateTableViewModel(IMessenger messenger,IPopServices popServices ,IMessagingServices messagingServices, INavigationServices navigationServices,
        ToyotaEmployeeModel toyotaEmployeeModel, ToyotaProcessModel toyotaProcessModel, CreateTableModel createTableModel)
    {
        _createTableModel = createTableModel;
        _toyotaEmployeeModel = toyotaEmployeeModel;
        _toyotaProcessModel = toyotaProcessModel;
        _popServices = popServices;
        _messenger = messenger;
        _messagingServices = messagingServices;
        _navigationServices = navigationServices;

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
    public async Task CreateTable()
    {
        if (_createTableModel.CreateTable())
        {

        }
        await _popServices.WaringPopup("Crio!", "Tabela criada!");
    }
    [RelayCommand]
    public async Task ConfigureTable()
    {
        var result = await _popServices.TableConfigPopup();

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
    void IRecipient<EmployeesCountChanged>.Receive(EmployeesCountChanged message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            EmployeesCount = message.Value;
        });
    }

    void IRecipient<ProcessesCountChanged>.Receive(ProcessesCountChanged message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            ProcessesCount = message.Value;
        });
    }

    [RelayCommand]
    public async Task SwitchToShowTables()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.ShowTable);
    }
    [RelayCommand]
    public async Task SwitchToRegister()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Register);
    }

    public void Receive(HiddedEmployeesCountChanged message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            HiddenEmployeesCount = message.Value;
        });
    }

    public void Receive(HiddedProcessesCountChanged message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            HiddenProcessesCount = message.Value;
        });
    }

    public void Receive(TableGroupRemovedMessage message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            Tables.Remove(message.Value);
        });
    }

    public void Receive(TableGroupAddedMessage message)
    {
        Tables.Add(message.Value);

    }
}
