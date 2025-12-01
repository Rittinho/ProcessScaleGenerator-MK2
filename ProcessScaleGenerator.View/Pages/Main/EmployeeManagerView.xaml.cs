using CommunityToolkit.Mvvm.Messaging;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.Shared.Messages;
using ProcessScaleGenerator.ViewModel.Pages.Main.Employeers;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class EmployeeManagerView : ContentView
{
    public EmployeeManagerView(EmployeeManagerViewModel employeeManagerViewModel)
    {
        InitializeComponent();
        BindingContext = employeeManagerViewModel;
    }

}