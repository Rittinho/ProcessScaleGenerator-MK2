using CommunityToolkit.Mvvm.Messaging.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Messages;

public class ProcessAddedMessage(ToyotaProcess addedProcess) : ValueChangedMessage<ToyotaProcess>(addedProcess)
{
}
public class ProcessRemovedMessage(ToyotaProcess removedProcess) : ValueChangedMessage<ToyotaProcess>(removedProcess)
{
}
public class ProcessesCountChanged(int processesCount) : ValueChangedMessage<int>(processesCount)
{
}
public class HiddedProcessesCountChanged(int hiddedProcessesCount) : ValueChangedMessage<int>(hiddedProcessesCount)
{
}