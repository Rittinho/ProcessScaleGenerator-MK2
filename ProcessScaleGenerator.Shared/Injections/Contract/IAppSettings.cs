using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Injections.Contract
{
    public interface IAppSettings
    {
        string RootPath();
        string ProcessesPath();
        string EmployeesPath();
        string TablesPath();
        string BackupsPath();
        string CurrentTheme();
        bool Autobackup();
        bool ChangeTheme(string theme);
    }
}
