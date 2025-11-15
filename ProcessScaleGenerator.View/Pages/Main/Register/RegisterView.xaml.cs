using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.Shared.Constants;
using ProcessScaleGenerator.Shared.Injections.Contract;
using ProcessScaleGenerator.ViewModel.Pages.Main.Register;

namespace ProcessScaleGenerator.View.Pages.Main.Register;

public partial class RegisterView : ContentPage
{
    public RegisterView(RegisterViewModel registerViewModel)
    {
        InitializeComponent();
        BindingContext = registerViewModel;
    }
}