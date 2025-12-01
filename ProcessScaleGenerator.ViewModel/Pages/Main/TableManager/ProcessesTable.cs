using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.ViewModel.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.TableManager
{
    public partial class TableManagerViewModel
    {
        [ObservableProperty]
        private string _searchProcessText;

        private bool _showProcessHiddeds = false;

        public List<ToyotaProcess> ProcessList { get; set; } = [];
        public ObservableCollection<ToyotaProcessWrapper> FiltredProcessList { get; set; } = [];

        partial void OnSearchProcessTextChanged(string value)
        {
            _showProcessHiddeds = false;

            var filtered = ProcessList
                .Where(x => x.Title.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                .Select(pros => new ToyotaProcessWrapper(pros))
                .ToList();

            FiltredProcessList.Clear();

            foreach (var item in filtered)
                FiltredProcessList.Add(item);

            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }
        [RelayCommand]
        public async Task ClearProcessHiddeds()
        {
            _showProcessHiddeds = false;

            ProcessList.ForEach(p =>
            {
                p.Hidded = false;
            });

            FiltredProcessList.Clear();

            foreach (var wrapper in ProcessList)
                FiltredProcessList.Add(new (wrapper));

            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }
        [RelayCommand]
        public async Task MarkAllProcessHiddeds()
        {
            _showProcessHiddeds = false;

            ProcessList.ForEach(p =>
            {
                p.Hidded = true;
            });

            FiltredProcessList.Clear();

            foreach (var wrapper in ProcessList)
                FiltredProcessList.Add(new(wrapper));

            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }
        [RelayCommand]
        public async Task ShowProcessHiddeds()
        {
            _showProcessHiddeds = !_showProcessHiddeds;

            List<ToyotaProcess> hiddeds = [];

            if (_showEmployeeHiddeds)
            {
                hiddeds = [.. ProcessList.Where(p => p.Hidded)];
            }
            else
            {
                hiddeds = ProcessList;
            }

            FiltredProcessList.Clear();

            foreach (var item in hiddeds)
                FiltredProcessList.Add(new (item));

            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }

        public void Receive(ProcessAddedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                ProcessList.Add(message.Value);
                FiltredProcessList.Add(new (message.Value));
            });
        }

        public void Receive(ProcessRemovedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                ProcessList.Remove(message.Value);

                var wrapperParaRemover = FiltredProcessList.FirstOrDefault(w => w.Model == message.Value);
                if (wrapperParaRemover != null)
                {
                    FiltredProcessList.Remove(wrapperParaRemover);
                }
            });
        }

        public void Receive(ProcessesCleaned message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                ProcessList.Clear();
                FiltredProcessList.Clear();
            });
        }
    }
}
