using CommunityToolkit.Mvvm.Messaging.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Messages;

public class EmployeeAddedMessage(ToyotaEmployee addedEmployee) : ValueChangedMessage<ToyotaEmployee>(addedEmployee)
{
}
public class EmployeeRemovedMessage(ToyotaEmployee removedEmployee) : ValueChangedMessage<ToyotaEmployee>(removedEmployee)
{
}
public class EmployeesCountChanged(int employeesCount) : ValueChangedMessage<int>(employeesCount)
{
}
public class HiddedEmployeesCountChanged(int HiddedEmployeesCount) : ValueChangedMessage<int>(HiddedEmployeesCount)
{
}
public class EmployeesCleaned(bool cleaned) : ValueChangedMessage<bool>(cleaned)
{
}

