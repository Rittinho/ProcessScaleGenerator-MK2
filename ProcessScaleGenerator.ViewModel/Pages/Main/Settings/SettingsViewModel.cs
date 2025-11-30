using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared;
using ProcessScaleGenerator.Shared.Constants;
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
            var settings = new SystemSettings(RootPath, BackupsPath, CurrentTheme, (bool)Autobackup);
            _repositoryServices.SaveSettings(settings);
        }
        public void LoadSettings()
        {
            CurrentTheme = _appSettings.CurrentTheme();
            RootPath = _appSettings.RootPath();
            BackupsPath = _appSettings.BackupsPath();
            Autobackup = _appSettings.Autobackup();
        }
        [RelayCommand]
        private async void LoadProcesses()
        {
            if (await _repositoryServices.LoadFileProcesses())
            {
                await _popServices.WaringPopup("Arquivos importados", "Verifique a lita");
            }
        }
        [RelayCommand]
        private async void LoadEmployees()
        {
            if (await _repositoryServices.LoadFileEmployeers())
            {
                await _popServices.WaringPopup("Arquivos importados", "Verifique a lita");
            }
        }
        [RelayCommand]
        private async void LoadTable()
        {
            if (await _repositoryServices.LoadFileTables())
            {
                await _popServices.WaringPopup("Arquivos importados", "Verifique a lita");
            }
        }
    }
}
