using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.TableManager
{
    public partial class TableManagerViewModel : ObservableObject
    {
        private readonly IRepositoryServices _repositoryServices;
        private readonly IMessenger _messenger;

        public TableManagerViewModel(IRepositoryServices repositoryServices, IMessenger messenger)
        {
            _repositoryServices = repositoryServices;
            _messenger = messenger;

            ProcessList = _repositoryServices.GetAllProcesses();
            EmployeeList = _repositoryServices.GetAllEmployees();

            FiltredProcessList = [.. ProcessList];
            FiltredEmployeeList = [.. EmployeeList];
        }
        public void SendMessages()
        {
            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }
    }
}
