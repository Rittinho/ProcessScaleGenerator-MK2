using ProcessScaleGenerator.ViewModel.Pages.Main;

namespace ProcessScaleGenerator.View.Pages;

public partial class MainPage : ContentPage
{
    int count = 0;
    bool totoya = false;
    public MainPage(MainPageViewModel mainPageViewModel)
	{
		InitializeComponent();
		BindingContext = mainPageViewModel;
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        count++;

        if (count == 10)
        {
            totoya = !totoya;
            if (totoya)
            {
                EsterEgg.Text = "TOTOYA";
                count = 0;
            }
            else
            {
                EsterEgg.Text = "TOYOTA";
                count = 0;
            }
        }
    }
}