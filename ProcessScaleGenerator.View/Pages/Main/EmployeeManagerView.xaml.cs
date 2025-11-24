
using ProcessScaleGenerator.ViewModel.Pages.Main.Employeers;

namespace ProcessScaleGenerator.View.Pages.Main;

public partial class EmployeeManagerView : ContentPage
{
	public EmployeeManagerView(EmployeeManagerViewModel employeeManagerViewModel)
    {
        InitializeComponent();
        BindingContext = employeeManagerViewModel;
    }
}