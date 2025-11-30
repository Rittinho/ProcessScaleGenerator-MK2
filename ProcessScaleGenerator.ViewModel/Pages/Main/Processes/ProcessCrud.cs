using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Model.Process;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Processes
{
    public partial class ProcessManagerViewModel
    {
        [RelayCommand]
        public async Task CreateNewProcess()
        {
            if (string.IsNullOrEmpty(Title))
            {
                await _popServices.WaringPopup(WarningTokens.EmptyFild);
                return;
            }

            try
            {
                _toyotaProcessModel!.CreateProcess(new(Icon, DateTime.Now.ToString(), Title, Description ?? "Sem descrição"));
            }
            catch
            {
                await _popServices.WaringPopup(WarningTokens.ExistingProcess);
                return;
            }

            await _popServices.WaringPopup(WarningTokens.CreateSuccess);

            ClearProcessFilds();
        }

        [RelayCommand]
        public async Task UpdateIcon()
        {
            var result = await _popServices.IconPickerPopup(Icon);

            Icon = result;
        }
        [RelayCommand]
        public async Task ShowProcess(ToyotaProcess? toyotaProcess)
        {
            if (toyotaProcess is null)
            {
                await _popServices.WaringPopup(WarningTokens.CorruptFile);
                return;
            }

            await _popServices.ShowProcessPopup(toyotaProcess!);
        }

        [RelayCommand]
        public async Task DeleteProcess(ToyotaProcess? toyotaProcess)
        {
            if (!await _popServices.ConfirmPopup(WarningTokens.DeleteProcess))
                return;

            if (_toyotaProcessModel!.DeleteProcess(toyotaProcess!))
                await _popServices.WaringPopup(WarningTokens.DeleteSuccess);

            ClearProcessFilds();
            SwitchMode(RegisterMode.Create);
        }

        [RelayCommand]
        public async Task UpdateProcess(ToyotaProcess? toyotaProcess)
        {
            _currentProcessInEdit = toyotaProcess;

            if (!CheckIfAnythingHasChangedProcess())
            {
                if (!await _popServices.ConfirmPopup(WarningTokens.DescarteUpdate))
                    return;

                ClearProcessFilds();
            }

            SwitchMode(RegisterMode.Edit);
            LoadProcessFilds();
        }
        [RelayCommand]
        public async Task SaveUpdateProcess()
        {
            if (string.IsNullOrEmpty(Title))
            {
                await _popServices.WaringPopup(WarningTokens.EmptyFild);
                return;
            }

            if (!CheckIfAnythingHasChangedProcess())
            {
                ClearProcessFilds();
                SwitchMode(RegisterMode.Create);
                return;
            }

            if (!await _popServices.ConfirmPopup(WarningTokens.UpdateProcess))
                return;

            try
            {
                _toyotaProcessModel!.UpdateProcess(_currentProcessInEdit, new(Icon, _currentProcessInEdit.CreationDate, Title, Description ?? "Sem descrição"));
            }
            catch
            {
                await _popServices.WaringPopup(WarningTokens.ExistingProcess);
                return;
            }

            await _popServices.WaringPopup(WarningTokens.UpdateSuccess);

            ClearProcessFilds();
            SwitchMode(RegisterMode.Create);
        }
        [RelayCommand]
        public async Task CancelUpdateProcess()
        {
            if (!CheckIfAnythingHasChangedProcess())
            {
                ClearProcessFilds();
                SwitchMode(RegisterMode.Create);
                return;
            }

            if (!await _popServices.ConfirmPopup(WarningTokens.DescarteUpdate))
                return;

            ClearProcessFilds();
            SwitchMode(RegisterMode.Create);
        }
    }
}