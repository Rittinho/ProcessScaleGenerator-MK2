using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;

namespace ProcessScaleGenerator.Model.Table;

public class CreateTableModel
{
    private readonly IRepositoryServices _repositoryServices;

    public CreateTableModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;
    }

    public List<ToyotaTableGroup> GetAllTables() => _repositoryServices.GetAllTables() ?? [];
    public ToyotaTableGroup GetFirstTable() => _repositoryServices.GetFirstTable();
    public ToyotaTableGroup GetLastTable() => _repositoryServices.GetLastTable();

    public ToyotaTableGroup CreateTable() => CreateRandomTables();
    public bool SaveTable(ToyotaTableGroup table)
    {
        if (table is null)
            return true;

        return _repositoryServices.SaveNewTableGroup(table);
    }
    public bool DeleteTable(ToyotaTableGroup toyotaTableGroup)
    {
        if (toyotaTableGroup == null)
            return false;

        return _repositoryServices.RemoveTableGroup(toyotaTableGroup);
    }
    public ToyotaTableGroup CreateRandomTables()
    {
        Random random = new();

        List<ToyotaEmployee> employeeList = [.._repositoryServices.GetAllEmployees().Where(x => x.Hidded == false)];
        List<ToyotaProcess> processList = [.._repositoryServices.GetAllProcesses().Where(x => x.Hidded == false)];

        int employeeHidded = _repositoryServices.GetAllEmployees().Where(x => x.Hidded).Count();
        int processHidded = _repositoryServices.GetAllProcesses().Where(x => x.Hidded).Count();

        List<ToyotaProcessTable> tables = [];

        List<ToyotaEmployee> randomizedList = [.. employeeList.OrderBy(x => random.Next())];

        int baseCount = employeeList.Count / processList.Count;
        int remainder = employeeList.Count % processList.Count;

        int currentIndex = 0;

        for (int i = 0; i < processList.Count; i++)
        {
            int count = baseCount + (i < remainder ? 1 : 0);

            List<ToyotaEmployee> employees = randomizedList.Skip(currentIndex).Take(count).ToList();

            var table = new ToyotaProcessTable(processList[i], employees);

            tables.Add(table);

            currentIndex += count;
        }

        ToyotaTableGroup result = new(DateTime.Now.ToString(), tables.Count, employeeList.Count, baseCount, processHidded, employeeHidded, tables);

        return result;
    }
}
