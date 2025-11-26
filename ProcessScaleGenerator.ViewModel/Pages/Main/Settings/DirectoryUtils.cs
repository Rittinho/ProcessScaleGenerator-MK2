using CommunityToolkit.Mvvm.ComponentModel;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Settings
{
    public partial class SettingsViewModel
    {
        [ObservableProperty]
        private string? _processPath;

        [ObservableProperty]
        private string? _employeePath;

        [ObservableProperty]
        private string? _tablePath;

        [ObservableProperty]
        private string? _backupPath;

    }
}
