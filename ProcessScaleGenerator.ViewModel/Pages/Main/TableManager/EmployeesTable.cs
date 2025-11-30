using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.Shared.ValueObjects;
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
        private string _searchEmployeeText;

        private bool _showEmployeeHiddeds = false;

        public List<ToyotaEmployee> EmployeeList { get; set; } = [];
        public ObservableCollection<ToyotaEmployee> FiltredEmployeeList { get; set; } = [];

        partial void OnSearchEmployeeTextChanged(string value)
        {
            _showEmployeeHiddeds = false;

            var filtered = EmployeeList
                .Where(x => x.Name.StartsWith(value, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FiltredEmployeeList.Clear();
            foreach (var item in filtered)
                FiltredEmployeeList.Add(item);

            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
        }

        [RelayCommand]
        public async Task ClearEmployeeHiddeds()
        {
            _showEmployeeHiddeds = false;

            EmployeeList.ForEach(p =>
            {
                p.Hidded = false;
            });

            FiltredEmployeeList.Clear();

            foreach (var item in EmployeeList)
                FiltredEmployeeList.Add(item);

            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
        }
        [RelayCommand]
        public async Task MarkAllEmployeeHiddeds()
        {
            _showEmployeeHiddeds = false;

            EmployeeList.ForEach(p =>
            {
                p.Hidded = true;
            });

            FiltredEmployeeList.Clear();

            foreach (var item in EmployeeList)
                FiltredEmployeeList.Add(item);

            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
        }

        [RelayCommand]
        public async Task ShowEmployeeHiddeds()
        {
            _showEmployeeHiddeds = !_showEmployeeHiddeds;

            List<ToyotaEmployee> hiddeds = [];

            if (_showEmployeeHiddeds)
            {
                hiddeds = [.. EmployeeList.Where(p => p.Hidded)];
            }
            else
            {
                hiddeds = EmployeeList;
            }

            FiltredEmployeeList.Clear();

            foreach (var item in hiddeds)
                FiltredEmployeeList.Add(item);

            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
        }
    }
}
