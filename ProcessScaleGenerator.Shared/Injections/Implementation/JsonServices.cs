using ProcessScaleGenerator.Shared.Injections.Contract;using ProcessScaleGenerator.Shared.ValueObjects;using System.Text;using System.Text.Encodings.Web;using System.Text.Json;

namespace ProcessScaleGenerator.Shared.Injections.Implementation;

public class JsonServices : IJsonServices
{
    JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    private readonly string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Toyota repository");

    private readonly string _tableFilePath = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Toyota repository"), "Tableas salvas");

    public JsonServices()
    {
        if (!Directory.Exists(_filePath))
            Directory.CreateDirectory(_filePath);

        if (!Directory.Exists(_tableFilePath))
            Directory.CreateDirectory(_tableFilePath);
    }

    public void SaveEmployeeJson(List<ToyotaEmployee> data)
    {
        var jsonPath = Path.Combine(_filePath, "employee.json");

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
        var jsonPath = Path.Combine(_filePath, "process.json");

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
        var cultura = new System.Globalization.CultureInfo("pt-BR");
        DateTime date = DateTime.ParseExact(data.CreationDate, "dd/MM/yyyy HH:mm:ss", cultura);
        var jsonPath = Path.Combine(_tableFilePath, $"table_group_{date:dd-MM-yyyy_HH-mm-ss}.json");

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
        var jsonPath = Path.Combine(_filePath, "employee.json");

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
                return null;
            }
        }

        return result;
    }
    public List<ToyotaProcess> LoadProcessJson()
    {
        var jsonPath = Path.Combine(_filePath, "process.json");

        if (!File.Exists(jsonPath))
            throw new Exception("Sem arquivos de tabela!");

        List<ToyotaProcess> result = [];

        using (StreamReader stream = new(jsonPath))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaProcess>>(json, options);
            }
            catch
            {
                return [];
            }
        }

        return result;
    }
    public List<ToyotaTableGroup> LoadTableGroupJson()
    {
        string[] files = Directory.GetFiles(_tableFilePath, "*.json");

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

    public void DeleteFileJson(string fileName)
    {
        var jsonPath = Path.Combine(_filePath, $"{fileName}.json");

        if (!File.Exists(jsonPath))
            throw new Exception("Arquivo não existe!");

        try
        {
            File.Delete(jsonPath);
        }
        catch (IOException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void DeleteTableFileJson(string creationDate)
    {
        var cultura = new System.Globalization.CultureInfo("pt-BR");
        DateTime date = DateTime.ParseExact(creationDate, "dd/MM/yyyy HH:mm:ss", cultura);
        var jsonPath = Path.Combine(_tableFilePath, $"table_group_{date:dd-MM-yyyy_HH-mm-ss}.json");

        if (!File.Exists(jsonPath))
            throw new Exception("Arquivo não existe!");

        try
        {
            File.Delete(jsonPath);
        }
        catch (IOException ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
