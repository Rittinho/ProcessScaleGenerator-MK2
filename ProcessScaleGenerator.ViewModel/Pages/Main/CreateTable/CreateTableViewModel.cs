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
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;

public partial class CreateTableViewModel : ObservableObject, IRecipient<EmployeesCountChanged>, IRecipient<ProcessesCountChanged>
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

        HiddenEmployeesCount = 0;
        HiddenProcessesCount = 1;

        _messenger.RegisterAll(this);

        EmployeesCount = _toyotaEmployeeModel.GetAllEmployees().Count;
        ProcessesCount = _toyotaProcessModel.GetAllProcesses().Count;

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
}
