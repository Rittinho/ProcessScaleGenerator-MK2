using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.ViewModel.Pages.Main.CreateTable;
using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

namespace ProcessScaleGenerator.View.Pages.Main.CreateTable;

public partial class CreateTableView : ContentPage
{
    public CreateTableView(CreateTableViewModel createTableViewModel)
    {
        InitializeComponent();
        BindingContext = createTableViewModel;
    }
}