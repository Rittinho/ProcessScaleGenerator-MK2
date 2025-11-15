using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Model.Table;

public class CreateTableModel
{
    private readonly IRepositoryServices _repositoryServices;

    public CreateTableModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;
    }

    public List<ToyotaTableGroup> GetAllTables() => _repositoryServices.GetAllTables();
    public ToyotaTableGroup GetFirstTable() => _repositoryServices.GetFirstTable();
    public ToyotaTableGroup GetLastTable() => _repositoryServices.GetLastTable();

    public bool CreateTable()
    {
        ToyotaTableGroup table = CreateRandomTables();

        if (table == null)
            return false;

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

        List<ToyotaEmployee> employeeList = _repositoryServices.GetAllEmployees();
        List<ToyotaProcess> processList = _repositoryServices.GetAllProcesses();

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

        ToyotaTableGroup result = new(DateTime.Now.ToString(), tables, tables.Count, employeeList.Count, baseCount, 0, 0);

        return result;
    }
}
