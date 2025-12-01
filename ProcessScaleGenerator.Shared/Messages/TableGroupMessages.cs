using CommunityToolkit.Mvvm.Messaging.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Messages;

public class TableGroupAddedMessage(ToyotaTableGroup addedTableGroup) : ValueChangedMessage<ToyotaTableGroup>(addedTableGroup)
{
}
public class TableGroupRemovedMessage(ToyotaTableGroup removedTableGroup) : ValueChangedMessage<ToyotaTableGroup>(removedTableGroup)
{
}
public class TableGroupCleaned(bool cleaned) : ValueChangedMessage<bool>(cleaned)
{
}