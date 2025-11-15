namespace ProcessScaleGenerator.Shared.ValueObjects;

public class ToyotaProcessTable(ToyotaProcess Process, List<ToyotaEmployee> Employees)
{
    public ToyotaProcess Process { get; set; } = Process;
    public List<ToyotaEmployee> Employees { get; set; } = Employees;
}
