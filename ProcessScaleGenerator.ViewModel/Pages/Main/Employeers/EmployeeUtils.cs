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
using System.Xml.Linq;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Employeers
{
    public partial class EmployeeManagerViewModel
    {
        private ToyotaEmployee? _currentEmployeeInEdit;

        public List<ToyotaEmployee> EmployeeList { get; set; } = [];
        public ObservableCollection<ToyotaEmployee> FiltredEmployeeList { get; set; } = [];

        [ObservableProperty]
        private string? _searchEmployeeText;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _position;

        public void Receive(EmployeeAddedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                EmployeeList.Add(message.Value);
                FiltredEmployeeList.Add(message.Value);
            });
        }

        public void Receive(EmployeeRemovedMessage message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                EmployeeList.Remove(message.Value);
                FiltredEmployeeList.Remove(message.Value);
            });
        }

        public void Receive(EmployeesCleaned message)
        {
            _messagingServices.BeginInvokeOnMainThread(() =>
            {
                FiltredEmployeeList.Clear();
            });
        }

        partial void OnSearchEmployeeTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                FiltredEmployeeList.Clear();
                foreach (var item in EmployeeList)
                    FiltredEmployeeList.Add(item);
                return;
            }

            var filtered = EmployeeList
                .Where(x => x.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FiltredEmployeeList.Clear();
            foreach (var item in filtered)
                FiltredEmployeeList.Add(item);
        }
        private bool CheckIfAnythingHasChangedEmployee()
        {
            return !(Name == _currentEmployeeInEdit!.Name && Position == _currentEmployeeInEdit.Position);
        }
        private void ClearEmployeeFilds()
        {
            Name = string.Empty;
            Position = null;
        }
        private void LoadEmployeeFilds()
        {
            Name = _currentEmployeeInEdit!.Name;
            Position = _currentEmployeeInEdit.Position;
        }
    }
}
