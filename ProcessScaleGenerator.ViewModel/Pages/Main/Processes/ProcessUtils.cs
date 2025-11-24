using CommunityToolkit.Mvvm.ComponentModel;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Processes
{
    public partial class ProcessManagerViewModel
    {
        private ToyotaProcess? _currentProcessInEdit;

        public List<ToyotaProcess> ProcessList { get; set; } = [];
        public ObservableCollection<ToyotaProcess> FiltredProcessList { get; set; } = [];

        [ObservableProperty]
        private string? _searchProcessText;

        [ObservableProperty]
        private IconParameters? _icon;

        [ObservableProperty]
        private string? _title;

        [ObservableProperty]
        private string? _description;

        public void Receive(ProcessAddedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                ProcessList.Add(message.Value);
                FiltredProcessList.Add(message.Value);
            });
        }

        public void Receive(ProcessRemovedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                ProcessList.Remove(message.Value);
                FiltredProcessList.Remove(message.Value);
            });
        }

        partial void OnSearchProcessTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FiltredProcessList.Clear();
                foreach (var item in ProcessList)
                    FiltredProcessList.Add(item);
                return;
            }

            var filtered = ProcessList
                .Where(x => x.Title.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FiltredProcessList.Clear();
            foreach (var item in filtered)
                FiltredProcessList.Add(item);
        }
        private bool CheckIfAnythingHasChangedProcess()
        {
            return !(Title == _currentProcessInEdit!.Title &&
                Description == _currentProcessInEdit.Description &&
                Icon == _currentProcessInEdit!.Icon);
        }
        private void ClearProcessFilds()
        {
            Icon = new IconParameters("Asterisk", "FFFFFF");
            Title = string.Empty;
            Description = string.Empty;
        }
        private void LoadProcessFilds()
        {
            Icon = _currentProcessInEdit!.Icon;
            Title = _currentProcessInEdit!.Title;
            Description = _currentProcessInEdit.Description;
        }
    }
}
