using ProcessScaleGenerator.Shared.Injections.Contract;using ProcessScaleGenerator.Shared.ValueObjects;using System.Text;using System.Text.Encodings.Web;using System.Text.Json;

namespace ProcessScaleGenerator.Shared.Injections.Implementation;

public class JsonServices : IJsonServices
{
    JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    private readonly IAppSettings _appSettings;
    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Process scale generator");

    public JsonServices(IAppSettings appSettings)
    {
        _appSettings = appSettings;
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
    }

    public void SaveEmployeeJson(List<ToyotaEmployee> data)
    {
        CheckEmployeersFolder();

        var jsonPath = Path.Combine(_appSettings.EmployeesPath(), "employee.json");

        if (File.Exists(jsonPath))
            File.Create(jsonPath).Dispose();

        using (StreamWriter streamWriter = new(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
    }
    public void SaveProcessJson(List<ToyotaProcess> data)
    {
        CheckProcessesFolder();

        var jsonPath = Path.Combine(_appSettings.ProcessesPath(), "process.json");

        if (File.Exists(jsonPath))
            File.Create(jsonPath).Dispose();

        using (StreamWriter streamWriter = new(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
    }
    public void SaveTableGroupJson(ToyotaTableGroup data)
    {
        CheckTableFolder();

        var cultura = new System.Globalization.CultureInfo("pt-BR");
        DateTime date = DateTime.ParseExact(data.CreationDate, "dd/MM/yyyy HH:mm:ss", cultura);
        var jsonPath = Path.Combine(_appSettings.TablesPath(), $"table_group_{date:dd-MM-yyyy_HH-mm-ss}.json");

        if (File.Exists(jsonPath))
            File.Create(jsonPath).Dispose();

        using (StreamWriter streamWriter = new(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
    }

    public List<ToyotaEmployee> LoadEmployeeJson()
    {
        CheckEmployeersFolder();

        var jsonPath = Path.Combine(_appSettings.EmployeesPath(), "employee.json");

        if (!File.Exists(jsonPath))
            throw new Exception("Sem arquivos de tabela!");

        List<ToyotaEmployee> result = [];

        using (StreamReader stream = new(jsonPath))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaEmployee>>(json, options)!;
            }
            catch
            {
                throw new JsonException();
            }
        }

        return result;
    }
    public List<ToyotaProcess> LoadProcessJson()
    {
        CheckProcessesFolder();

        var jsonPath = Path.Combine(_appSettings.ProcessesPath(), "process.json");

        if (!File.Exists(jsonPath))
            throw new Exception("Sem arquivos de tabela!");

        List<ToyotaProcess> result = [];

        using (StreamReader stream = new(jsonPath))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaProcess>>(json, options)!;
            }
            catch
            {
                throw new JsonException();
            }
        }

        return result;
    }
    public List<ToyotaTableGroup> LoadTableGroupJson()
    {
        CheckTableFolder();

        string[] files = Directory.GetFiles(_appSettings.TablesPath(), "*.json");

        if (files.Length == 0)
            throw new Exception("Sem arquivos de tabela!");

        List<ToyotaTableGroup> result = [];

        foreach (var json in files)
        {
            using (StreamReader stream = new(json))
            {
                string jsonToRead = stream.ReadToEnd();
                try
                {
                    result.Add(JsonSerializer.Deserialize<ToyotaTableGroup>(jsonToRead, options)!);
                }
                catch
                {
                    continue;
                }
            }
        }

        return result;
    }

    public List<ToyotaEmployee> LoadFileEmployeeJson(string pathFile)
    {
        CheckEmployeersFolder();

        if (!File.Exists(pathFile))
            throw new Exception("Sem arquivos de tabela!");

        if (Path.GetFileName(pathFile) != "employee.json")
            throw new Exception("Arquivo diferente");

        List<ToyotaEmployee> result = [];

        using (StreamReader stream = new(pathFile))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaEmployee>>(json, options)!;
            }
            catch
            {
                throw new JsonException();
            }
        }

        return result;
    }
    public List<ToyotaProcess> LoadFileProcessJson(string pathFile)
    {
        CheckProcessesFolder();

        if (!File.Exists(pathFile))
            throw new Exception("Arquivo com erro ou corrompido");

        if (Path.GetFileName(pathFile) != "process.json")
            throw new Exception("Arquivo diferente");

        List<ToyotaProcess> result = [];

        using (StreamReader stream = new(pathFile))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaProcess>>(json, options)!;
            }
            catch
            {
                throw new JsonException();
            }
        }

        return result;
    }
    public ToyotaTableGroup LoadFileTableGroupJson(string pathFile)
    {
        CheckTableFolder();

        if (!File.Exists(pathFile))
            throw new Exception("Arquivo diferente");

        ToyotaTableGroup result;

        using (StreamReader stream = new(pathFile))
        {
            string jsonToRead = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<ToyotaTableGroup>(jsonToRead, options)!;
            }
            catch
            {
                throw new JsonException();
            }
        }

        return result;
    }

    public void SaveSettingsJson(SystemSettings data)
    {
        var jsonPath = Path.Combine(_filePath, "AppSettings.json");

        using (StreamWriter streamWriter = new(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
    }

    public void DeleteTableFileJson(string creationDate)
    {
        var cultura = new System.Globalization.CultureInfo("pt-BR");
        DateTime date = DateTime.ParseExact(creationDate, "dd/MM/yyyy HH:mm:ss", cultura);
        var jsonPath = Path.Combine(_appSettings.TablesPath(), $"table_group_{date:dd-MM-yyyy_HH-mm-ss}.json");

        if (!File.Exists(jsonPath))
            throw new FileLoadException();

        try
        {
            File.Delete(jsonPath);
        }
        catch (IOException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Utils
    public void CheckProcessesFolder()
    {
        if (!Directory.Exists(_appSettings.RootPath()))
            Directory.CreateDirectory(_appSettings.RootPath());

        if (!Directory.Exists(_appSettings.ProcessesPath()))
            Directory.CreateDirectory(_appSettings.ProcessesPath());
    }
    public void CheckEmployeersFolder()
    {
        if (!Directory.Exists(_appSettings.EmployeesPath()))
            Directory.CreateDirectory(_appSettings.EmployeesPath());
    }
    public void CheckTableFolder()
    {
        if (!Directory.Exists(_appSettings.TablesPath()))
            Directory.CreateDirectory(_appSettings.TablesPath());
    }
    public void CheckBackupFolder()
    {
        if (!Directory.Exists(_appSettings.BackupsPath()))
            Directory.CreateDirectory(_appSettings.BackupsPath());
    }
    #endregion
}
