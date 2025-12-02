using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Data_log;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.Generic;
using ToyotaProcessManager.Services.Injections.Contract;
using static System.Net.Mime.MediaTypeNames;

namespace ProcessScaleGenerator.Shared.Injections.Implementation.Repository;

public partial class RepositoryServices : IRepositoryServices
{
    private readonly IJsonServices _jsonServices;
    private readonly IFolderStorage _folderStorage;
    private readonly IAppSettings _appSettings;
    private readonly IPopServices _popServices;
    private readonly IMessenger _messenger;

    private readonly List<ToyotaEmployee>? _employeeData;
    private readonly List<ToyotaProcess>? _processData;
    private readonly List<ToyotaTableGroup>? _tableData;

    private static readonly object _locker = new object();

    public RepositoryServices(IJsonServices jsonServices, IMessenger messenger, IFolderStorage folderStorage, IPopServices popServices, IAppSettings appSettings)
    {
        _jsonServices = jsonServices;
        _popServices = popServices;
        _appSettings = appSettings;
        _folderStorage = folderStorage;
        _messenger = messenger;

        lock (_locker)
        {
            try
            {
                _processData = _jsonServices.LoadProcessJson();
            }
            catch(Exception ex)
            {
                _processData = [];

                SendLog.Log(ex);
            }
            try
            {
                _employeeData = _jsonServices.LoadEmployeeJson();
            }
            catch(Exception ex)
            {
                _employeeData = [];

                SendLog.Log(ex);
            }
            try
            {
                _tableData = _jsonServices.LoadTableGroupJson();
            }
            catch(Exception ex)
            {
                _tableData = [];

                SendLog.Log(ex);
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

    public void SaveSettings(SystemSettings data) => _jsonServices.SaveSettingsJson(data);
}
