using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.ViewModel.Modal.Forms.TableConfigModal;

namespace ProcessScaleGenerator.View.Modal.Forms;

public partial class TableConfigModal : Popup<ToyotaTableConfiguration>
{
    public TableConfigModal(TableConfigModalViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
    [RelayCommand]
    private async Task CancelButton() => await CloseAsync();

    //[RelayCommand]
    //private async Task SaveButton() => await CloseAsync(new ToyotaTableConfiguration([..ProcessList],[..EmployeeList], [..ProcessList], [..EmployeeList]));
}