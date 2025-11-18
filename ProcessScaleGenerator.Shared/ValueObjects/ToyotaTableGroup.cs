namespace ProcessScaleGenerator.Shared.ValueObjects;

public class ToyotaTableGroup(string CreationDate, int TotalTables,
    int TotalEmployers, int MediaEmployersPerProcess, int HiddenProcessCount, int HiddenEmployersCount,
    List<ToyotaProcessTable> TableGroup)
{
    public string CreationDate { get; set; } = CreationDate;
    public int HiddenProcessCount { get; set; } = HiddenProcessCount;
    public int HiddenEmployersCount { get; set; } = HiddenEmployersCount;
    public int TotalTables { get; set; } = TotalTables;
    public int TotalProcess { get; set; } = TotalTables;
    public int TotalEmployers { get; set; } = TotalEmployers;
    public int MediaEmployersPerProcess { get; set; } = MediaEmployersPerProcess;
    public List<ToyotaProcessTable> TableGroup { get; set; } = TableGroup;
}
