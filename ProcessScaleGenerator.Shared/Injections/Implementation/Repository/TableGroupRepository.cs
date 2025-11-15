using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

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
            _jsonServices.SaveTableGroupJson(_tableData);
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

            _jsonServices.SaveTableGroupJson(_tableData);
            _messenger.Send(new TableGroupRemovedMessage(tableGroup));
        }

        return true;
    }
}