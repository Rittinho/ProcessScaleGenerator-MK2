using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Model.Employee;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Data_log;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Employeers
{
    public partial class EmployeeManagerViewModel
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
            catch(Exception ex)
            {
                await _popServices.WaringPopup(WarningTokens.ExistingProcess);
                SendLog.Log(ex);
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
            catch (Exception ex)
            {
                await _popServices.WaringPopup(WarningTokens.ExistingEmployee);
                SendLog.Log(ex);
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
}
