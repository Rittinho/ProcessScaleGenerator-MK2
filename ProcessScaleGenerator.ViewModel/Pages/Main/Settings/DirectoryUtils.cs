using CommunityToolkit.Mvvm.ComponentModel;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Settings
{
    public partial class SettingsViewModel
    {
        [ObservableProperty]
        public string? _rootPath;
        [ObservableProperty]
        public string? _backupsPath;
    }
}
