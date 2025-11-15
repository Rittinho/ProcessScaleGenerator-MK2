namespace ProcessScaleGenerator.Shared.ValueObjects;

public record ToyotaTableConfiguration(List<ToyotaProcess> ProcessesList, List<ToyotaEmployee> EmployeesList,
    List<ToyotaProcess> HiddenProcesses, List<ToyotaEmployee> HiddenEmployees);
