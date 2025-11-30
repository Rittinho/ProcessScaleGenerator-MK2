using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.Shared.Injections.Contract;

public interface IJsonServices
{
    void SaveEmployeeJson(List<ToyotaEmployee> data);
    void SaveProcessJson(List<ToyotaProcess> data);
    void SaveTableGroupJson(ToyotaTableGroup data);

    List<ToyotaEmployee> LoadEmployeeJson();
    List<ToyotaProcess> LoadProcessJson();
    List<ToyotaTableGroup> LoadTableGroupJson();

    List<ToyotaEmployee> LoadFileEmployeeJson(string pathFile);
    List<ToyotaProcess> LoadFileProcessJson(string pathFile);
    ToyotaTableGroup LoadFileTableGroupJson(string pathFile);

    void SaveSettingsJson(SystemSettings data);

    //void DeleteFileJson(string fileName);
    void DeleteTableFileJson(string creationDate);
}
