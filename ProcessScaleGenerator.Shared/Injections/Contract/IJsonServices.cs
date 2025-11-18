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

    void DeleteFileJson(string fileName);
    void DeleteTableFileJson(string creationDate);
}
