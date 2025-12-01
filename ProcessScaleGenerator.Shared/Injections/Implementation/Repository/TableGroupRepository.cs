using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProcessScaleGenerator.Shared.Injections.Implementation.Repository;

public partial class RepositoryServices
{
    public List<ToyotaTableGroup> GetAllTables() => _tableData;
    public ToyotaTableGroup GetFirstTable() => _tableData.FirstOrDefault();
    public ToyotaTableGroup GetLastTable() => _tableData.LastOrDefault();
    public bool SaveNewTableGroup(ToyotaTableGroup newTableGroup)
    {
        if (newTableGroup is null)
            return false;

        lock (_locker)
        {
            _tableData.Add(newTableGroup);
            _jsonServices.SaveTableGroupJson(newTableGroup);
            _messenger.Send(new TableGroupAddedMessage(newTableGroup));
        }

        return true;
    }
    public bool RemoveTableGroup(ToyotaTableGroup tableGroup)
    {
        if (tableGroup is null)
            return false;

        lock (_locker)
        {
            if (!_tableData.Remove(tableGroup))
                return false;

            _jsonServices.DeleteTableFileJson(tableGroup.CreationDate);
            _messenger.Send(new TableGroupRemovedMessage(tableGroup));
        }

        return true;
    }
    public async Task<bool> LoadFileTables()
    {
        var pathResult = await _folderStorage.GetSingleFileInFolder();

        if (string.IsNullOrEmpty(pathResult))
            return false;

        var result = _jsonServices.LoadFileTableGroupJson(pathResult);

        if (result is null)
            return false;

        lock (_locker)
        {   
            _tableData!.Add(result);

            _jsonServices.SaveTableGroupJson(result);
            _messenger.Send(new TableGroupAddedMessage(result));

            return true;
        }
    }
    public int RemoveAllTableGroup()
    {
        if (_tableData.Count == 0)
            throw new Exception("Processos já esta vazio!");

        string[] files = Directory.GetFiles(_appSettings.TablesPath(), "*.json");

        if (files.Length == 0)
            throw new Exception("Processos já esta vazio!");

        List<ToyotaTableGroup> result = [];

        string valide = @"table_group_\d{2}-\d{2}-\d{4}_\d{2}-\d{2}-\d{2}.json";

        int deletedCount = 0;   

        foreach (var json in files)
        {
            if (!Regex.IsMatch(json, valide))
            {
                continue;
            }
            else
            {
                try
                {
                    File.Delete(json);
                }
                catch (IOException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            deletedCount++;
        }

        _tableData.Clear();
        _messenger.Send(new TableGroupCleaned(true));

        return deletedCount;
    }
}