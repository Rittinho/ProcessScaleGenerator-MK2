using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Implementation.Repository;

public partial class RepositoryServices
{
    public List<ToyotaProcess> GetAllProcesses() => _processData;
    public ToyotaProcess GetFirstProcess() => _processData.FirstOrDefault();
    public ToyotaProcess GetLastProcess() => _processData.LastOrDefault();
    public List<ToyotaProcess> GetProcessByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("O valor fornecido é nulo!");

        return [.. _processData.Where(p => p.Title.ToLower().StartsWith(name.ToLower()))];
    }
    public bool SaveNewProcess(ToyotaProcess newProcess)
    {
        if (newProcess is null)
            return false;

        lock (_locker)
        {
            _processData.Add(newProcess);
            _jsonServices.SaveProcessJson(_processData);
            _messenger.Send(new ProcessAddedMessage(newProcess));
            _messenger.Send(new ProcessesCountChanged(_processData.Count));
        }

        return true;
    }
    public bool RemoveProcess(ToyotaProcess process)
    {
        if (process is null)
            return false;

        lock (_locker)
        {
            if (!_processData.Remove(process))
                return false;

            _jsonServices.SaveProcessJson(_processData);
            _messenger.Send(new ProcessRemovedMessage(process));
            _messenger.Send(new ProcessesCountChanged(_processData.Count));
        }

        return true;
    }

}