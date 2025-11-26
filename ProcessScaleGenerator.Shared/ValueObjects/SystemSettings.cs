using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.ValueObjects;

public class SystemSettings(string processesPath, string employeesPath, string tablesPath, string backupsPath, string currentTheme, bool saveToExit, bool confirmToDelete, bool autobackup)
{
    public string ProcessesPath { get; set; } = processesPath;
    public string EmployeesPath { get; set; } = employeesPath;
    public string TablesPath { get; set; } = tablesPath;
    public string BackupsPath { get; set; } = backupsPath;
    public string CurrentTheme { get; set; } = currentTheme;
    public bool SaveToExit { get; set; } = saveToExit;
    public bool ConfirmToDelete { get; set; } = confirmToDelete;
    public bool Autobackup { get; set; } = autobackup;
}

