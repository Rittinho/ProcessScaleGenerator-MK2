using CommunityToolkit.Mvvm.Messaging.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
