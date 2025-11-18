using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

public partial class TableConfigModalViewModel
{
    [ObservableProperty]
    private string _searchEmployeeText;

    private bool _showEmployeeHiddeds = false;

    public List<ToyotaEmployee> EmployeeList { get; set; } = [];
    public ObservableCollection<ToyotaEmployee> FiltredEmployeeList { get; set; } = [];
    public List<ToyotaEmployee> HiddenEmployeeList { get; set; } = [];

    partial void OnSearchEmployeeTextChanged(string value)
    {
        _showEmployeeHiddeds = false;

        var filtered = EmployeeList
            .Where(x => x.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FiltredEmployeeList.Clear();
        foreach (var item in filtered)
            FiltredEmployeeList.Add(item);
    }
    [RelayCommand]
    public async Task ClearEmployeeHiddeds()
    {
        _showEmployeeHiddeds = false;

        EmployeeList.ForEach(p =>
        {
            p.Hidded = false;
        });

        FiltredEmployeeList.Clear();

        foreach (var item in EmployeeList)
            FiltredEmployeeList.Add(item);
    }
    [RelayCommand]
    public async Task ShowEmployeeHiddeds()
    {
        _showEmployeeHiddeds = !_showEmployeeHiddeds;

        List<ToyotaEmployee> hiddeds = [];

        if (_showEmployeeHiddeds)
        {
            hiddeds = [.. EmployeeList.Where(p => p.Hidded)];
        }
        else
        {
            hiddeds = EmployeeList;
        }

        FiltredEmployeeList.Clear();

        foreach (var item in hiddeds)
            FiltredEmployeeList.Add(item);

    }
}