using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.View.Modal.Description;

public partial class EmployeeDescriptionModal : Popup
{
    public string Name { get; set; }
    public string Position { get; set; }

    public EmployeeDescriptionModal(ToyotaEmployee toyotaEmployee)
    {
        InitializeComponent();

        Name = toyotaEmployee.Name;
        Position = toyotaEmployee.Position;

        BindingContext = this;
    }
    [RelayCommand]
    public async Task CloseShow() => await CloseAsync();
}