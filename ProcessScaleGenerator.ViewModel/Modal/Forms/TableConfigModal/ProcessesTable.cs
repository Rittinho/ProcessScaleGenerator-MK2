using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

public partial class TableConfigModalViewModel
{
    [ObservableProperty]
    private string _searchProcessText;

    private bool _showProcessHiddeds = false;

    public List<ToyotaProcess> ProcessList { get; set; } = [];
    public ObservableCollection<ToyotaProcess> FiltredProcessList { get; set; } = [];
    public List<ToyotaProcess> HiddenProcessList { get; set; } = [];

    partial void OnSearchProcessTextChanged(string value)
    {
        _showProcessHiddeds = false;
        var filtered = ProcessList
            .Where(x => x.Title.StartsWith(value, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FiltredProcessList.Clear();
        
        foreach (var item in filtered)
            FiltredProcessList.Add(item);
    }
    [RelayCommand]
    public async Task ClearProcessHiddeds()
    {
        _showProcessHiddeds = false;

        ProcessList.ForEach(p => 
        {
            p.Hidded = false;
        });

        FiltredProcessList.Clear();

        foreach (var item in ProcessList)
            FiltredProcessList.Add(item);
    }
    [RelayCommand]
    public async Task ShowProcessHiddeds()
    {
        _showProcessHiddeds = !_showProcessHiddeds;

        List<ToyotaProcess> hiddeds = [];

        if (_showProcessHiddeds)
        {
            hiddeds = [..ProcessList.Where(p => p.Hidded)];
        }
        else
        {
            hiddeds = ProcessList;
        }

        FiltredProcessList.Clear();

        foreach (var item in hiddeds)
            FiltredProcessList.Add(item);
    }
}