using ProcessScaleGenerator.Shared.Injections.Contract;using ProcessScaleGenerator.Shared.ValueObjects;using System.Text;using System.Text.Encodings.Web;using System.Text.Json;

namespace ProcessScaleGenerator.Shared.Injections.Implementation;

public class JsonServices : IJsonServices
{
    JsonSerializerOptions options = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    private string _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Toyota repository");

    public JsonServices()
    {
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
    }

    public void SaveEmployeeJson(List<ToyotaEmployee> data)
    {
        var jsonPath = Path.Combine(_filePath, "employee.json");

        if (File.Exists(jsonPath))
            File.Create(jsonPath).Dispose();

        using (StreamWriter streamWriter = new StreamWriter(jsonPath, false, Encoding.UTF8))
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

        using (StreamWriter streamWriter = new StreamWriter(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
    }
    public void SaveTableGroupJson(List<ToyotaTableGroup> data)
    {
        var jsonPath = Path.Combine(_filePath, "table-group.json");

        if (File.Exists(jsonPath))
            File.Create(jsonPath).Dispose();

        using (StreamWriter streamWriter = new StreamWriter(jsonPath, false, Encoding.UTF8))
        {
            var json = JsonSerializer.Serialize(data, options);
            streamWriter.Write(json);
        }
        throw new NotImplementedException();
    }
    public List<ToyotaEmployee> LoadEmployeeJson()
    {
        var jsonPath = Path.Combine(_filePath, "employee.json");

        if (!File.Exists(jsonPath))
            return null;

        List<ToyotaEmployee> result = null;

        using (StreamReader stream = new(jsonPath))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaEmployee>>(json, options);
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
            return null;

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
        var jsonPath = Path.Combine(_filePath, "table-group.json");

        if (!File.Exists(jsonPath))
            return null;

        List<ToyotaTableGroup> result = null;

        using (StreamReader stream = new(jsonPath))
        {
            string json = stream.ReadToEnd();
            try
            {
                result = JsonSerializer.Deserialize<List<ToyotaTableGroup>>(json, options);
            }
            catch
            {
                return null;
            }
        }

        return result;
    }
}
