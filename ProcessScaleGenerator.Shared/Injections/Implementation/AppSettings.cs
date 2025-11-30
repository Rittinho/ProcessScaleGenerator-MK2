using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.Shared.Injections.Implementation
{
    public class AppSettings : IAppSettings
    {
        JsonSerializerOptions options = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator");

        private readonly SystemSettings _settings;

        public AppSettings()
        {
            _settings = LoadSettingsJson();
        }

        public string CurrentTheme() => _settings.CurrentTheme;
        public string BackupsPath() => _settings.BackupsPath;
        public string RootPath() => _settings.RootPath;

        public string EmployeesPath() => Path.Combine(_settings.RootPath, "Colaboradores registrados");
        public string ProcessesPath() => Path.Combine(_settings.RootPath, "Processos salvos");
        public string TablesPath() => Path.Combine(_settings.RootPath, "Tabelas criadas");

        public bool Autobackup() => _settings.Autobackup;

        public SystemSettings LoadSettingsJson()
        {
            var jsonPath = Path.Combine(_filePath, "AppSettings.json");

            SystemSettings result;

            if (!File.Exists(jsonPath))
            {
                result = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator"),
                        Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator"), "Backups"),
                        "System", true);
            }

            using (StreamReader stream = new(jsonPath))
            {
                string json = stream.ReadToEnd();

                try
                {
                    result = JsonSerializer.Deserialize<SystemSettings>(json, options);
                }
                catch
                {
                    result = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator"),
                        Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator"), "Backups"),
                        "System", true);
                }
            }

            return result;
        }
    }
}
