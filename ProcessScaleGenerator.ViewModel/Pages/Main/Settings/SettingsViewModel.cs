using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Data_log;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyotaProcessManager.Services.Injections.Contract;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Settings
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IRepositoryServices _repositoryServices;
        private readonly IPopServices _popServices;
        private readonly IAppSettings _appSettings;

        [ObservableProperty]
        public string? _currentTheme;
        [ObservableProperty]
        public bool? _autobackup;

        public SettingsViewModel(IRepositoryServices repositoryServices, IPopServices popServices, IAppSettings appSettings)
        {
            _repositoryServices = repositoryServices;
            _popServices = popServices;
            _appSettings = appSettings;
            LoadSettings();
        }
        public void SaveSettings()
        {
            var settings = new SystemSettings(RootPath!, BackupsPath!, CurrentTheme!, (bool)Autobackup!);
            _repositoryServices.SaveSettings(settings);
        }
        public void LoadSettings()
        {
            CurrentTheme = _appSettings.CurrentTheme();
            RootPath = _appSettings.RootPath();
            BackupsPath = _appSettings.BackupsPath();
            Autobackup = _appSettings.Autobackup();
        }
        partial void OnCurrentThemeChanged(string? value)
        {
            _appSettings.ChangeTheme(value);
        }
        [RelayCommand]
        private async Task LoadProcesses()
        {
            int result = await _repositoryServices.LoadFileProcesses();

            try
            {
                if (result != -1)
                {
                    await _popServices.WaringPopup($"{result} Processos importados", "Verifique a lita");
                }
            }
            catch (Exception ex)
            {
                SendLog.Log(ex);
            }
        }
        [RelayCommand]
        private async Task LoadEmployees()
        {
            int result = await _repositoryServices.LoadFileEmployeers();

            try
            {
                if (result != -1)
                {
                    await _popServices.WaringPopup($"{result} Colaboradores importados", "Verifique a lita");
                }
            }
            catch (Exception ex)
            {
                SendLog.Log(ex);
            }
        }
        [RelayCommand]
        private async Task LoadTable()
        {
            try
            {
                if (await _repositoryServices.LoadFileTables())
                {
                    await _popServices.WaringPopup("Tabela importada", "Verifique a lita");
                }
            }
            catch (Exception ex)
            {
                SendLog.Log(ex);
            }
        }
        [RelayCommand]
        private async Task DeleteAllProcesses()
        {
            if (!await _popServices.ConfirmPopup("Deseja apagar todos os processos?","Está ação não tem volta!"))
                return;

            int result = 0;

            try
            {
                result = _repositoryServices.RemoveAllProcess();
            }
            catch (Exception ex)
            {
                await _popServices.WaringPopup(ex.Message, "Verifique a lita");
                SendLog.Log(ex);
                return;
            }
            
            await _popServices.WaringPopup($"{result} Processos deletados", "Verifique a lita");
        }
        [RelayCommand]
        private async Task DeleteAllEmployees()
        {
            if (!await _popServices.ConfirmPopup("Deseja apagar todos os Colaboradores?", "Está ação não tem volta!"))
                return;

            int result = 0;

            try
            {
                result = _repositoryServices.RemoveAllEmployee();
            }
            catch (Exception ex)
            {
                await _popServices.WaringPopup(ex.Message, "Verifique a lita");
                SendLog.Log(ex);
                return;
            }

            await _popServices.WaringPopup($"{result} Colaboradores deletados", "Verifique a lita");
        }
        [RelayCommand]
        private async Task DeleteAllTable()
        {
            if (!await _popServices.ConfirmPopup("Deseja apagar todos as tabelas?", "Está ação não tem volta!"))
                return;

            int result = 0;

            try
            {
                result = _repositoryServices.RemoveAllTableGroup();
            }
            catch (Exception ex)
            {
                await _popServices.WaringPopup(ex.Message, "Verifique a lita");
                SendLog.Log(ex);
                return;
            }
            
            await _popServices.WaringPopup($"{result} tabelas deletadas", "Verifique a lita");
        }
    }
}