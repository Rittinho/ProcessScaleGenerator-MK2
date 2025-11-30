namespace ProcessScaleGenerator.Shared.ValueObjects;

public class SystemSettings(string rootPath, string backupsPath, string currentTheme, bool autobackup)
{
    public string RootPath { get; set; } = rootPath;
    public string BackupsPath { get; set; } = backupsPath;
    public string CurrentTheme { get; set; } = currentTheme;
    public bool Autobackup { get; set; } = autobackup;
}

