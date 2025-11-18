using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.ViewModel.Modal.Forms.IconPicker;

namespace ProcessScaleGenerator.View.Modal.Forms;

public partial class IconSelecterModal : Popup<IconParameters>
{
    public IconSelecterModal(IconParameters iconParameters, IconPickerModalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        WeakReferenceMessenger.Default.Register<Result>(this, (r, msg) =>
        {
            CloseAsync(msg.Icon);
            WeakReferenceMessenger.Default.Unregister<Result>(this);
        });
    }

    [RelayCommand]
    private async Task CancelButton() => await CloseAsync();
}