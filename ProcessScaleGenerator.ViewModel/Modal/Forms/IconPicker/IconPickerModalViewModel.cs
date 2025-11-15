using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using System.Collections.ObjectModel;

namespace ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;

public partial class IconPickerModalViewModel : ObservableObject
{
    private readonly string[] _iconColors =
    [
        "00FF00",
        "9ACD32",
        "FFFF00",
        "FFD700",
        "FFA500",
        "FF4500",
        "FF0000",
        "C71585",
        "800080",
        "4B0082",
        "0000FF",
        "00CED1"
    ];

    public ObservableCollection<string> ColorsList { get; } = [];
    public ObservableCollection<string> IconList { get; set; } = [];

    [ObservableProperty]
    public string _selectedUnicode;

    [ObservableProperty]
    public string _selectedColorCode;

    public IconPickerModalViewModel()
    {
        GerateIcons();

        SelectedUnicode = "Asterisk";
        SelectedColorCode = _iconColors[0];
    }
    private void GerateIcons()
    {
        foreach (var c in FontAwesome.FASolid)
            IconList.Add(c.Value);

        foreach (var d in _iconColors)
            ColorsList.Add(d);
    }

    [ObservableProperty]
    private string? _searchEmployeeText;

    [RelayCommand]
    private void IconSelectionChanged(object unicodeSelected)
    {
        if (unicodeSelected is string colorCode && !string.IsNullOrEmpty(colorCode))
            SelectedUnicode = FontAwesome.FASolid.FirstOrDefault(kvp => kvp.Value == unicodeSelected).Key;

    }

}
