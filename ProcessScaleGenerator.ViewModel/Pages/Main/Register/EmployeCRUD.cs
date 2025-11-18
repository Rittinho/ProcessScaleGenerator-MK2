using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Register;

public partial class RegisterViewModel
{
    [RelayCommand]
    public async Task CreateNewEmployee()
    {
        if (string.IsNullOrEmpty(Name))
        {
            await _popServices.WaringPopup(WarningTokens.EmptyFild);
            return;
        }

        if (string.IsNullOrEmpty(Position))
        {
            await _popServices.WaringPopup(WarningTokens.EmptyFild);
            return;
        }

        try
        {
            _toyotaEmployeeModel.CreateEmployee(new(DateTime.Now.ToString(), Name, Position));
        }
        catch
        {
            await _popServices.WaringPopup(WarningTokens.ExistingProcess);
            return;
        }

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
        _currentEmployeeInEdit = toyotaEmployee;

        if (!CheckIfAnythingHasChangedEmployee())
        {
            if (!await _popServices.ConfirmPopup(WarningTokens.DescarteUpdate))
                return;

            ClearEmployeeFilds();
        }

        SwitchMode(RegisterMode.Edit);
        LoadEmployeeFilds();
    }
    [RelayCommand]
    public async Task SaveUpdateEmployee()
    {
        if (string.IsNullOrEmpty(Name))
        {
            await _popServices.WaringPopup(WarningTokens.EmptyFild);
            return;
        }

        if (string.IsNullOrEmpty(Position))
        {
            await _popServices.WaringPopup(WarningTokens.EmptyFild);
            return;
        }

        if (!CheckIfAnythingHasChangedEmployee())
        {
            ClearEmployeeFilds();
            SwitchMode(RegisterMode.Create);
            return;
        }

        if (!await _popServices.ConfirmPopup(WarningTokens.UpdateEmployee))
            return;

        try
        {
            _toyotaEmployeeModel!.UpdateEmployee(_currentEmployeeInEdit!, new(_currentEmployeeInEdit.CreationDate, Name, Position));
        }
        catch
        {
            await _popServices.WaringPopup(WarningTokens.ExistingEmployee);
            return;
        }

        await _popServices.WaringPopup(WarningTokens.UpdateSuccess);

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

