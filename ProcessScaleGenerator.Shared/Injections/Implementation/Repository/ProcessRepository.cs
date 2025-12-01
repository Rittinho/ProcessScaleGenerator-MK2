using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Diagnostics;

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
    public async Task<int> LoadFileProcesses()
    {
        var pathResult = await _folderStorage.GetSingleFileInFolder();

        if (string.IsNullOrEmpty(pathResult))
            return -1;

        int addedCount = 0;

        List<ToyotaProcess> result;
        try
        {
            result = _jsonServices.LoadFileProcessJson(pathResult);
        }
        catch (Exception ex) 
        {
            _popServices.WaringPopup(ex.Message,"Tentar novamente");
            return -1;
        }

        if (result is null)
            return -1;

        lock (_locker)
        {
            foreach (var process in result)
            {
                _processData!.Add(process);
                _messenger.Send(new ProcessAddedMessage(process));

                addedCount++;
            }

            _messenger.Send(new ProcessesCountChanged(_processData.Count));
        }
        return addedCount;
    }

    public int RemoveAllProcess()
    {
        if (_processData.Count == 0)
            throw new Exception("Processos já esta vazio!");

        int count = _processData.Count;

        _processData.Clear();
        _messenger.Send(new ProcessesCleaned(true));

        _jsonServices.SaveProcessJson(_processData);
        _messenger.Send(new ProcessesCountChanged(_processData.Count));

        return count;
    }
}