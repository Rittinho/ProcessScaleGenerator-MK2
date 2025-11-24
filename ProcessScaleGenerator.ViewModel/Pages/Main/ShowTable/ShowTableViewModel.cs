using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Model.Table;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

public partial class ShowTableViewModel : ObservableObject
{
    private readonly CreateTableModel _createTableModel;

    public ObservableCollection<ToyotaProcessTable> Tables { get; set; } = [];
    public ToyotaTableGroup LastTableGroup { get; set; }

    private readonly IRepositoryServices _repositoryServices;
    private readonly INavigationServices _navigationServices;

    public ObservableCollection<ToyotaTableGroup> Grup;
    public ShowTableViewModel(IRepositoryServices repositoryServices, INavigationServices navigationServices)
    {
        _repositoryServices = repositoryServices;
        _navigationServices = navigationServices;

        LastTableGroup = _repositoryServices.GetLastTable();

        try
        {
            Tables = [.. LastTableGroup.TableGroup];
        }
        catch
        {
            Tables = [];
        }
    }
    [RelayCommand]
    public async Task SwitchToDashboard()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Dashboard);
    }
    [RelayCommand]
    public async Task SwitchToTableManager()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.TableManager);
    }
    [RelayCommand]
    public async Task SwitchToProcessesManager()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Processes);
    }
    [RelayCommand]
    public async Task SwitchToEmployeersManager()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Employeers);
    }
    [RelayCommand]
    public async Task SwitchToSettings()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Settings);
    }
}