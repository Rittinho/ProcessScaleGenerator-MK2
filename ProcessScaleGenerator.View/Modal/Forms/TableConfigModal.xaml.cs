using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

namespace ProcessScaleGenerator.View.Modal.Forms;

public partial class TableConfigModal : Popup<HiddenFromTable>
{
    public TableConfigModal(TableConfigModalViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

        WeakReferenceMessenger.Default.Register<Result>(this, (r, msg) =>
        {
            CloseAsync(msg.HiddenFromTable);
            WeakReferenceMessenger.Default.Unregister<Result>(this);
        });
    }
}