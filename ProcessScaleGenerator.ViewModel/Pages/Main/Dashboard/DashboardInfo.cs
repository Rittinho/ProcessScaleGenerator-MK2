using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.Dashboard
{
    public partial class DashboardViewModel
    {
        [ObservableProperty]
        private int? _processesCount;

        [ObservableProperty]
        private int? _employeesCount;

        [ObservableProperty]
        private int? _hiddenProcessesCount;

        [ObservableProperty]
        private int? _hiddenEmployeesCount;

        //quantidade de processos de funcionarios
        void IRecipient<EmployeesCountChanged>.Receive(EmployeesCountChanged message) => _messagingServices.BeginInvokeOnMainThread(() => { EmployeesCount = message.Value; });
        void IRecipient<ProcessesCountChanged>.Receive(ProcessesCountChanged message) => _messagingServices.BeginInvokeOnMainThread(() => { ProcessesCount = message.Value; });

        //Processos e funcionarios ocultados
        public void Receive(HiddedEmployeesCountChanged message) => _messagingServices.BeginInvokeOnMainThread(() => { HiddenEmployeesCount = message.Value; });
        public void Receive(HiddedProcessesCountChanged message) => _messagingServices.BeginInvokeOnMainThread(() => { HiddenProcessesCount = message.Value; });
    }
}
