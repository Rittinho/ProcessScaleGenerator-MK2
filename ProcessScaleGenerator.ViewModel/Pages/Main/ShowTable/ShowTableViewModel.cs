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

    private readonly IRepositoryServices _repositoryServices;
    private readonly INavigationServices _navigationServices;

    public ObservableCollection<ToyotaTableGroup> Grup;
    public ShowTableViewModel(IRepositoryServices repositoryServices, INavigationServices navigationServices)
    {
        _repositoryServices = repositoryServices;
        _navigationServices = navigationServices;
        Grup = [.._repositoryServices.GetAllTables()];
    }
    [RelayCommand]
    public async Task SwitchToRegister()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.Register);
    }
    [RelayCommand]
    public async Task SwitchToCreateTables()
    {
        _navigationServices.GoToPageAsync(RegisteredPages.CreateTable);
    }
}
