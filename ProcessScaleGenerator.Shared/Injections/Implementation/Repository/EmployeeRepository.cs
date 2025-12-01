using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Implementation.Repository;

public partial class RepositoryServices
{
    public List<ToyotaEmployee> GetAllEmployees() => _employeeData;
    public List<ToyotaEmployee> GetEmployeeByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O valor fornecido é nulo!");

        return [.. _employeeData.Where(p => p.Name.ToLower().StartsWith(name.ToLower()))];
    }
    public List<ToyotaEmployee> GetEmployeeByPosition(string position)
    {
        if (string.IsNullOrEmpty(position))
            throw new ArgumentNullException("O valor fornecido é nulo!");

        return [.. _employeeData.Where(p => p.Name.ToLower().StartsWith(position.ToLower()))];
    }
    public ToyotaEmployee GetFirstEmployee() => _employeeData.FirstOrDefault();
    public ToyotaEmployee GetLastEmployee() => _employeeData.LastOrDefault();
    public bool SaveNewEmployee(ToyotaEmployee newEmployee)
    {
        if (newEmployee is null)
            return false;

        lock (_locker)
        {
            _employeeData.Add(newEmployee);
            _jsonServices.SaveEmployeeJson(_employeeData);
            _messenger.Send(new EmployeeAddedMessage(newEmployee));
            _messenger.Send(new EmployeesCountChanged(_employeeData.Count));
        }

        return true;
    }
    public bool RemoveEmployee(ToyotaEmployee employee)
    {
        if (employee is null)
            return false;

        lock (_locker)
        {
            if (!_employeeData.Remove(employee))
                return false;

            _jsonServices.SaveEmployeeJson(_employeeData);
            _messenger.Send(new EmployeeRemovedMessage(employee));
            _messenger.Send(new EmployeesCountChanged(_employeeData.Count));
        }

        return true;
    }
    public async Task<int> LoadFileEmployeers()
    {
        var pathResult = await _folderStorage.GetSingleFileInFolder();

        if (string.IsNullOrEmpty(pathResult))
            return -1;

        List<ToyotaEmployee> result;

        int addedCount = 0;

        try
        {
            result = _jsonServices.LoadFileEmployeeJson(pathResult);
        }
        catch (Exception ex)
        {
            _popServices.WaringPopup(ex.Message, "Tentar novamente");
            return -1;
        }

        if (result is null)
            return -1;

        lock (_locker)
        {
            foreach (var employer in result)
            {
                _employeeData!.Add(employer);
                _messenger.Send(new EmployeeAddedMessage(employer));
                addedCount++;
            }

            _messenger.Send(new EmployeesCountChanged(_employeeData.Count));
        }

        return addedCount;
    }

    public int RemoveAllEmployee()
    {
        if (_employeeData.Count == 0)
            throw new Exception("Processos já esta vazio!");

        int count = _employeeData.Count;

        _employeeData.Clear();
        _messenger.Send(new EmployeesCleaned(true));

        _jsonServices.SaveEmployeeJson(_employeeData);
        _messenger.Send(new EmployeesCountChanged(_employeeData.Count));

        return count;
    }
}