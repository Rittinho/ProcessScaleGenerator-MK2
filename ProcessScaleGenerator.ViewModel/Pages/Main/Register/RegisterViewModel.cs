using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Employee;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Injections.Implementation;
using ProcessScaleGenerator.Shared.Messages;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Register;

public partial class RegisterViewModel : ObservableObject, IRecipient<EmployeeAddedMessage>, IRecipient<EmployeeRemovedMessage>, IRecipient<ProcessAddedMessage>, IRecipient<ProcessRemovedMessage>
{
    private readonly IMessenger _messenger;
    private readonly IMessagingServices _messagingServices;
    private readonly INavigationServices _navigationServices;
    private readonly IPopServices _popServices;

    private readonly ToyotaEmployeeModel? _toyotaEmployeeModel;
    private readonly ToyotaProcessModel? _toyotaProcessModel;

    public enum RegisterMode { Create, Edit }
    public enum RegisterPanel { Process, Employee }

    [ObservableProperty]
    public bool? _isInEditMode;

    [ObservableProperty]
    public bool? _isInCreateMode;

    [ObservableProperty]
    public bool? _isInProcessPanel;

    [ObservableProperty]
    public bool? _isInEmployeePanel;

    public RegisterViewModel(ToyotaEmployeeModel toyotaEmployeeModel, ToyotaProcessModel toyotaProcessModel, IMessenger messenger,
        IPopServices popServices, IMessagingServices messagingServices, INavigationServices navigationServices)
    {
        _toyotaEmployeeModel = toyotaEmployeeModel;
        _toyotaProcessModel = toyotaProcessModel;
        _messagingServices = messagingServices;
        _navigationServices = navigationServices;
        _popServices = popServices;
        _messenger = messenger;

        SwitchMode(RegisterMode.Create);
        SwitchPanel(RegisterPanel.Process);
        ClearProcessFilds();
        ClearEmployeeFilds();
        _messenger.RegisterAll(this);
        LoadInitialData();
    }
    private void LoadInitialData()
    {
        var initialEmployees = _toyotaEmployeeModel.GetAllEmployees();
        var initialProcesses = _toyotaProcessModel.GetAllProcesses();

        EmployeeList.Clear();
        ProcessList.Clear();
        FiltredEmployeeList.Clear();
        FiltredProcessList.Clear();

        foreach (var employee in initialEmployees)
        {
            EmployeeList.Add(employee);
            FiltredEmployeeList.Add(employee);
        }

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
    private void SwitchPanel(RegisterPanel mode)
    {
        switch (mode)
        {
            case RegisterPanel.Process:
                IsInProcessPanel = true;
                IsInEmployeePanel = false;
                break;
            case RegisterPanel.Employee:
                IsInProcessPanel = false;
                IsInEmployeePanel = true;
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    private void SwitchToProcessPanel()
    {
        SwitchPanel(RegisterPanel.Process);
        SwitchMode(RegisterMode.Create);
    }
    [RelayCommand]
    private void SwitchToEmployeePanel()
    {
        SwitchPanel(RegisterPanel.Employee);
        SwitchMode(RegisterMode.Create);
    }
    [RelayCommand]
    public async Task SwitchToShowTables()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.ShowTable);
    }
    [RelayCommand]
    public async Task SwitchToCreateTables()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.CreateTable);
    }

}
