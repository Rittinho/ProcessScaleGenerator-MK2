using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Model.Process;

public class ToyotaProcessModel
{
    private readonly IRepositoryServices _repositoryServices;

    public ToyotaProcessModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;
    }

    public List<ToyotaProcess> GetAllProcesses() => _repositoryServices.GetAllProcesses();
    public ToyotaProcess GetFirstProcess() => _repositoryServices.GetFirstProcess();
    public ToyotaProcess GetLastProcess() => _repositoryServices.GetLastProcess();
    public List<ToyotaProcess> GetProcessByName(string name) => _repositoryServices.GetProcessByName(name);

    public void CreateProcess(ToyotaProcess toyotaProcess)
    {
        if (toyotaProcess is null)
            throw new NullReferenceException();

        if (CheckSameProcess(toyotaProcess))
            throw new Exception("Já existe esse processo!");

        _repositoryServices.SaveNewProcess(toyotaProcess);
    }
    public void UpdateProcess(ToyotaProcess oldToyotaProcess, ToyotaProcess newToyotaProcess)
    {
        if (oldToyotaProcess is null)
            throw new NullReferenceException();

        if (newToyotaProcess is null)
            throw new NullReferenceException();

        if (CheckSameProcess(newToyotaProcess))
            throw new Exception("Já existe esse processo!");

        _repositoryServices.RemoveProcess(oldToyotaProcess);
        _repositoryServices.SaveNewProcess(newToyotaProcess);
    }
    public bool DeleteProcess(ToyotaProcess toyotaProcess)
    {
        if (toyotaProcess is null)
            throw new NullReferenceException();

        return _repositoryServices.RemoveProcess(toyotaProcess);
    }

    public bool CheckSameProcess(ToyotaProcess toyotaProcess)
    {
        foreach (var process in _repositoryServices.GetAllProcesses())
            if (process.Title == toyotaProcess.Title &&
                process.Description == toyotaProcess.Description &&
                process.Icon == toyotaProcess.Icon)
                return true;

        return false;
    }
}
