using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Data_log;
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
            try
            {
                CurrentGrup = _createTableModel.CreateTable();
            }
            catch(Exception ex)
            {
                await _popServices.WaringPopup("Não á como gerar", "não tem processos ou colaboradores registrados");
                SendLog.Log(ex);
                return;
            }

            OnPreviewTableCreated?.Invoke(CurrentGrup.TableGroup);
        }
        public void Receive(TableGroupRemovedMessage message) => _messagingServices.BeginInvokeOnMainThread(() => { Tables.Remove(message.Value); });
        public void Receive(TableGroupAddedMessage message) => _messagingServices.BeginInvokeOnMainThread(() => { Tables.Add(message.Value); });
    }
}
