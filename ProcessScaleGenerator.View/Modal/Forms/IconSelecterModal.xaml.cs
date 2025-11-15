using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;

namespace ProcessScaleGenerator.View.Modal.Forms;

public partial class IconSelecterModal : Popup<IconParameters>
{

    public IconSelecterModal(IconParameters iconParameters, IconPickerModalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    public void SelectIcon(object sender, SelectionChangedEventArgs e)
    {
        //if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        //{
        //    string value = e.CurrentSelection[0] as string;
        //    _selectedUnicode = FontAwesome.FASolid.FirstOrDefault(kvp => kvp.Value == value).Key;
        //}

        //Icon.Unicode = _selectedUnicode!;
    }

    public void SelectColor(object sender, SelectionChangedEventArgs e)
    {
        //if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
        //{
        //    if (e.CurrentSelection[0] is Color color)
        //    {
        //        _selectedColorCode = color.ToString();
        //        Icon.ColorCode = _selectedColorCode;
        //    }
        //}
    }

    [RelayCommand]
    private async Task CancelButton() => await CloseAsync();

    //[RelayCommand]
    //private async Task SaveButton() => await CloseAsync(new IconParameters(_selectedUnicode, _selectedColorCode));

}