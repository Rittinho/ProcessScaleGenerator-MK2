using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Implementation.Repository;

public partial class RepositoryServices : IRepositoryServices
{
    private readonly IJsonServices _jsonServices;
    private readonly IMessenger _messenger;

    private readonly List<ToyotaEmployee>? _employeeData;
    private readonly List<ToyotaProcess>? _processData;
    private readonly List<ToyotaTableGroup>? _tableData;

    private static readonly object _locker = new object();

    public RepositoryServices(IJsonServices jsonServices, IMessenger messenger)
    {
        _jsonServices = jsonServices;
        _messenger = messenger;

        lock (_locker)
        {
            try
            {
                _tableData = _jsonServices.LoadTableGroupJson();
            }
            catch
            {
                _messenger.Send(new TableGrupsNull(true));
                _tableData = [];
            }
            try
            {
                _employeeData = _jsonServices.LoadEmployeeJson();
            }
            catch
            {
                _messenger.Send(new EmployeesNull(true));
                _employeeData = [];
            }
            try
            {
                _processData = _jsonServices.LoadProcessJson();
            }
            catch
            {
                _messenger.Send(new ProcessesNull(true));
                _processData = [];
            }
        }
    }

    public void SaveAllData()
    {
        lock (_locker)
        {
            _jsonServices.SaveEmployeeJson(_employeeData);
            _jsonServices.SaveProcessJson(_processData);
        }
    }
}
