using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.ViewModel.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessScaleGenerator.ViewModel.Pages.Main.TableManager
{
    public partial class TableManagerViewModel : ObservableObject, IRecipient<ProcessAddedMessage>, IRecipient<ProcessRemovedMessage>, IRecipient<EmployeeAddedMessage>, IRecipient<EmployeeRemovedMessage>, IRecipient<EmployeesCleaned>, IRecipient<ProcessesCleaned>
    {
        private readonly IRepositoryServices _repositoryServices;
        private readonly IMessagingServices _messagingServices;
        private readonly IMessenger _messenger;

        public TableManagerViewModel(IRepositoryServices repositoryServices, IMessenger messenger, IMessagingServices messagingServices)
        {
            _repositoryServices = repositoryServices;
            _messagingServices = messagingServices;
            _messenger = messenger;

            ProcessList = _repositoryServices.GetAllProcesses();
            EmployeeList = _repositoryServices.GetAllEmployees();

            FiltredProcessList = [.. ProcessList.Select(pros => new ToyotaProcessWrapper(pros))];
            FiltredEmployeeList = [.. EmployeeList.Select(emp => new ToyotaEmployeeWrapper(emp))];

            _messenger.RegisterAll(this);
        }
        public void SendMessages()
        {
            _messenger.Send(new HiddedEmployeesCountChanged(EmployeeList.Where(x => x.Hidded).Count()));
            _messenger.Send(new HiddedProcessesCountChanged(ProcessList.Where(x => x.Hidded).Count()));
        }
    }
}
