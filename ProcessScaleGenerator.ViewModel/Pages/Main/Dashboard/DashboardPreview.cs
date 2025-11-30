using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard
{
    public partial class DashboardViewModel
    {
        public event Action<List<ToyotaProcessTable>> OnPreviewTableCreated;
        public event Action<bool> OnTableSaved;

        public ToyotaTableGroup CurrentGrup;

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

            await _popServices.WaringPopup("Crio!", "Tabela criada!");

            OnPreviewTableCreated?.Invoke(CurrentGrup.TableGroup);
        }
        public void Receive(TableGroupRemovedMessage message) => _messagingServices.BeginInvokeOnMainThread(() => { Tables.Remove(message.Value); });
        public void Receive(TableGroupAddedMessage message) => _messagingServices.BeginInvokeOnMainThread(() => { Tables.Add(message.Value); });
    }
}
