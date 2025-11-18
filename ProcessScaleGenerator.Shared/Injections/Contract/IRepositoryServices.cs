using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IRepositoryServices
{
    void SaveAllData();

    bool SaveNewEmployee(ToyotaEmployee newEmployee);
    bool RemoveEmployee(ToyotaEmployee employee);
    List<ToyotaEmployee> GetAllEmployees();
    ToyotaEmployee GetFirstEmployee();
    ToyotaEmployee GetLastEmployee();
    List<ToyotaEmployee> GetEmployeeByName(string name);
    List<ToyotaEmployee> GetEmployeeByPosition(string position);

    bool SaveNewProcess(ToyotaProcess newProcess);
    bool RemoveProcess(ToyotaProcess process);
    List<ToyotaProcess> GetAllProcesses();
    ToyotaProcess GetFirstProcess();
    ToyotaProcess GetLastProcess();
    List<ToyotaProcess> GetProcessByName(string name);

    bool SaveNewTableGroup(ToyotaTableGroup newTableGroup);
    bool RemoveTableGroup(ToyotaTableGroup tableGroup);
    List<ToyotaTableGroup> GetAllTables();
    ToyotaTableGroup GetFirstTable();
    ToyotaTableGroup GetLastTable();
}
