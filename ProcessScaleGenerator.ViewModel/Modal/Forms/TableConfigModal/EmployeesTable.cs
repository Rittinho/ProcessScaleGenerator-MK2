using CommunityToolkit.Mvvm.ComponentModel;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

public partial class TableConfigModalViewModel
{
    [ObservableProperty]
    private string _searchEmployeeText;

    public List<ToyotaEmployee> EmployeeList { get; set; } = [];
    public ObservableCollection<ToyotaEmployee> FiltredEmployeeList { get; set; } = [];
    public List<ToyotaEmployee> HiddenEmployeeList { get; set; } = [];

    partial void OnSearchEmployeeTextChanged(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            FiltredEmployeeList.Clear();
            foreach (var item in EmployeeList)
                FiltredEmployeeList.Add(item);
            return;
        }

        var filtered = EmployeeList
            .Where(x => x.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FiltredEmployeeList.Clear();
        foreach (var item in filtered)
            FiltredEmployeeList.Add(item);
    }
}