using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;

public partial class CreateTableViewModel
{
    public event Action<List<ToyotaProcessTable>> OnPreviewTableCreated;
    public event Action<bool> OnTableSaved;

    public ToyotaTableGroup CurrentGrup;
    public List<ToyotaProcessTable> CurrentTable { get; set; } = [];
    [RelayCommand]
    public async Task SaveTable()
    {
        if (!_createTableModel.SaveTable(CurrentGrup))
            await _popServices.WaringPopup("Erro ao criar!", "Tabela não criada!");

        await _popServices.WaringPopup("Salvo!", "Tabela Salva!");
        OnTableSaved?.Invoke(true);
    }
    [RelayCommand]
    public async Task CreateTable()
    {
        CurrentGrup = _createTableModel.CreateTable();

        CurrentTable = CurrentGrup.TableGroup;

        await _popServices.WaringPopup("Crio!", "Tabela criada!");

        OnPreviewTableCreated?.Invoke(CurrentTable);
    }
    public void Receive(TableGroupRemovedMessage message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            Tables.Remove(message.Value);
        });
    }
    public void Receive(TableGroupAddedMessage message)
    {
        _messagingServices.BeginInvokeOnMainThread(() =>
        {
            Tables.Add(message.Value);
        });
    }
}