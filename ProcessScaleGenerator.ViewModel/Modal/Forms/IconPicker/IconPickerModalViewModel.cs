using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.ValueObjects;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Globalization;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;

public partial class IconPickerModalViewModel : ObservableObject
{
    public ObservableCollection<string> IconList { get; set; } = [];

    [ObservableProperty]
    public string _selectedUnicode;

    public IconPickerModalViewModel()
    {
        GerateIcons();

        SelectedUnicode = "Asterisk";
    }
    private void GerateIcons()
    {
        foreach (var c in FontAwesome.FASolid)
            IconList.Add(c.Value);
    }

    [RelayCommand]
    private void IconSelectionChanged(object unicodeSelected)
    {
        if (unicodeSelected is string colorCode && !string.IsNullOrEmpty(colorCode))
            SelectedUnicode = FontAwesome.FASolid.FirstOrDefault(kvp => kvp.Value == unicodeSelected).Key;
    }
    [RelayCommand]
    private void Confirm()
    {
        WeakReferenceMessenger.Default.Send(new Result(new(SelectedUnicode)));
    }
}
public record Result(IconParameters Icon);
