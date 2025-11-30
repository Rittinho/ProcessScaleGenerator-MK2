using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IRepositoryServices
{
    void SaveAllData();
    void SaveSettings(SystemSettings data);

    bool SaveNewEmployee(ToyotaEmployee newEmployee);
    Task<bool> LoadFileEmployeers();
    bool RemoveEmployee(ToyotaEmployee employee);
    List<ToyotaEmployee> GetAllEmployees();
    ToyotaEmployee GetFirstEmployee();
    ToyotaEmployee GetLastEmployee();
    List<ToyotaEmployee> GetEmployeeByName(string name);
    List<ToyotaEmployee> GetEmployeeByPosition(string position);

    bool SaveNewProcess(ToyotaProcess newProcess);
    Task<bool> LoadFileProcesses();
    bool RemoveProcess(ToyotaProcess process);
    List<ToyotaProcess> GetAllProcesses();
    ToyotaProcess GetFirstProcess();
    ToyotaProcess GetLastProcess();
    List<ToyotaProcess> GetProcessByName(string name);

    bool SaveNewTableGroup(ToyotaTableGroup newTableGroup);
    Task<bool> LoadFileTables();
    bool RemoveTableGroup(ToyotaTableGroup tableGroup);
    List<ToyotaTableGroup> GetAllTables();
    ToyotaTableGroup GetFirstTable();
    ToyotaTableGroup GetLastTable();
}
