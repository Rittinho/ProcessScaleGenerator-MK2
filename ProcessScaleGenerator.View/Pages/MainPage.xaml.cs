using ProcessScaleGenerator.ViewModel.Pages.Main;

namespace ProcessScaleGenerator.View.Pages;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
	}
}