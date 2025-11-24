using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ProcessScaleGenerator.Shared.Messages
{
    public class EmployeesNull(bool active) : ValueChangedMessage<bool>(active)
    {
    }
    public class ProcessesNull(bool active) : ValueChangedMessage<bool>(active)
    {
    }
    public class TableGrupsNull(bool active) : ValueChangedMessage<bool>(active)
    {
    }
}
