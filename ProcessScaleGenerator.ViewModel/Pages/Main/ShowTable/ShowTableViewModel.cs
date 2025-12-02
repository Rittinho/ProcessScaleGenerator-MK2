using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Data_log;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

public partial class ShowTableViewModel : ObservableObject
{
    public event Action<List<ToyotaProcessTable>> OnPreviewTableCreated;
    public List<ToyotaProcessTable> Tables { get; set; } = [];

    private readonly IRepositoryServices _repositoryServices;

    public ShowTableViewModel(IRepositoryServices repositoryServices)
    {
        _repositoryServices = repositoryServices;

        try
        {
            Tables = _repositoryServices.GetLastTable().TableGroup;
        }
        catch(Exception ex)
        {
            Tables = [];
            SendLog.Log(ex);
        }
        OnPreviewTableCreated?.Invoke(Tables);
    }
}