using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Model.Employee;

public class ToyotaEmployeeModel
{
    private readonly IRepositoryServices _repositoryServices;

    public ToyotaEmployeeModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;
    }

    public List<ToyotaEmployee> GetAllEmployees() => _repositoryServices.GetAllEmployees();
    public ToyotaEmployee GetFirstEmployee() => _repositoryServices.GetFirstEmployee();
    public ToyotaEmployee GetLastEmployee() => _repositoryServices.GetLastEmployee();
    public List<ToyotaEmployee> GetEmployeeByName(string name) => _repositoryServices.GetEmployeeByName(name);
    public List<ToyotaEmployee> GetEmployeeByPosition(string position) => _repositoryServices.GetEmployeeByPosition(position);

    public void CreateEmployee(ToyotaEmployee toyotaEmployee)
    {
        if (toyotaEmployee == null)
            throw new Exception("Valor nulo!");

        if (CheckSameEmployee(toyotaEmployee))
            throw new Exception("Já existe esse processo!");

        _repositoryServices.SaveNewEmployee(toyotaEmployee);
    }
    public void UpdateEmployee(ToyotaEmployee oldToyotaEmployee, ToyotaEmployee newToyotaEmployee)
    {
        if (oldToyotaEmployee == null)
            throw new Exception("Valor nulo!");

        if (newToyotaEmployee == null)
            throw new Exception("Valor nulo!");

        if (CheckSameEmployee(newToyotaEmployee))
            throw new Exception("Já existe esse Colaborador!");

        _repositoryServices.RemoveEmployee(oldToyotaEmployee);
        _repositoryServices.SaveNewEmployee(newToyotaEmployee);
    }
    public bool DeleteEmployee(ToyotaEmployee toyotaEmployee)
    {
        if (toyotaEmployee == null)
            return false;

        return _repositoryServices.RemoveEmployee(toyotaEmployee);
    }

    public bool CheckSameEmployee(ToyotaEmployee toyotaEmployee)
    {
        foreach (var employee in _repositoryServices.GetAllEmployees())
            if (employee.Name == toyotaEmployee.Name && employee.Position == toyotaEmployee.Position)
                return true;

        return false;
    }
}
