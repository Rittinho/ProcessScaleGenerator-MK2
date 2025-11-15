using CommunityToolkit.Mvvm.ComponentModel;
using ProcessScaleGenerator.Shared.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

public partial class TableConfigModalViewModel : ObservableObject
{
    private readonly IRepositoryServices _repositoryServices;

    public TableConfigModalViewModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;


        ProcessList = _repositoryServices.GetAllProcesses();
        EmployeeList = _repositoryServices.GetAllEmployees();

        FiltredProcessList = [.. ProcessList];
        FiltredEmployeeList = [..EmployeeList];
    }
}
