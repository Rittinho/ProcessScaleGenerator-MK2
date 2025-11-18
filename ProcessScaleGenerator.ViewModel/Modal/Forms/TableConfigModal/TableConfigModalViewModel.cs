using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

public partial class TableConfigModalViewModel : ObservableObject
{
    private readonly IRepositoryServices _repositoryServices;
    private readonly IMessenger _messenger;

    public TableConfigModalViewModel(IRepositoryServices repositoryServices, IMessenger messenger)
    {
        _repositoryServices = repositoryServices;

        ProcessList = _repositoryServices.GetAllProcesses();
        EmployeeList = _repositoryServices.GetAllEmployees();

        FiltredProcessList = [.. ProcessList];
        FiltredEmployeeList = [.. EmployeeList];

        _messenger = messenger;
    }
    [RelayCommand]
    private void BackButton()
    {
        var processes = ProcessList.Where(p => p.Hidded).ToList();
        var employee = EmployeeList.Where(e => e.Hidded).ToList();
        WeakReferenceMessenger.Default.Send(new Result(new(processes,employee)));
        _messenger.Send(new HiddedEmployeesCountChanged(employee.Count));
        _messenger.Send(new HiddedProcessesCountChanged(processes.Count));
    }
}
public record Result(HiddenFromTable HiddenFromTable);