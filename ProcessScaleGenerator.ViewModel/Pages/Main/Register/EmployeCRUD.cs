using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Register;

public partial class RegisterViewModel
{
    [RelayCommand]
    public async Task CreateNewEmployee()
    {
        ToyotaEmployee newEmployee = new(DateTime.Now.ToString(), Name, Position);

        _toyotaEmployeeModel.CreateEmployee(newEmployee);

        _currentEmployeeInEdit = newEmployee;

        await _popServices.WaringPopup(WarningTokens.CreateSuccess);

        ClearEmployeeFilds();
    }

    [RelayCommand]
    public async Task ShowEmployee(ToyotaEmployee? toyotaEmployee)
    {
        if (toyotaEmployee is null)
        {
            await _popServices.WaringPopup(WarningTokens.CorruptFile);
            return;
        }

        await _popServices.ShowEmployeePopup(toyotaEmployee!);
    }

    [RelayCommand]
    public async Task DeleteEmployee(ToyotaEmployee? toyotaEmployee)
    {
        if (!await _popServices.ConfirmPopup(WarningTokens.DeleteEmployee))
            return;

        if (_toyotaEmployeeModel!.DeleteEmployee(toyotaEmployee!))
            await _popServices.WaringPopup(WarningTokens.DeleteSuccess);

        ClearEmployeeFilds();
        SwitchMode(RegisterMode.Create);
    }

    [RelayCommand]
    public async Task UpdateEmployee(ToyotaEmployee? toyotaEmployee)
    {
        if (!CheckIfAnythingHasChangedEmployee())
        {
            if (!await _popServices.ConfirmPopup(WarningTokens.DescarteUpdate))
                return;

            ClearEmployeeFilds();
        }

        _currentEmployeeInEdit = toyotaEmployee;
        SwitchMode(RegisterMode.Edit);
        LoadEmployeeFilds();
    }
    [RelayCommand]
    public async Task SaveUpdateEmployee()
    {
        ToyotaEmployee newEmployee = new(_currentEmployeeInEdit.CreationDate, Title, Description);

        if (!CheckIfAnythingHasChangedEmployee())
        {
            ClearEmployeeFilds();
            SwitchMode(RegisterMode.Create);
            return;
        }

        if (!await _popServices.ConfirmPopup(WarningTokens.UpdateEmployee))
            return;

        _toyotaEmployeeModel!.UpdateEmployee(_currentEmployeeInEdit!, newEmployee);

        ClearEmployeeFilds();
        SwitchMode(RegisterMode.Create);
    }
    [RelayCommand]
    public async Task CancelUpdateEmployee()
    {
        if (!CheckIfAnythingHasChangedEmployee())
        {
            ClearEmployeeFilds();
            SwitchMode(RegisterMode.Create);
            return;
        }

        if (!await _popServices.ConfirmPopup(WarningTokens.DescarteUpdate))
            return;

        ClearEmployeeFilds();
        SwitchMode(RegisterMode.Create);
    }
}

