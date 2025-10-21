namespace Projekt_Siłownia;

public partial class TrainerPage : ContentPage
{
	public TrainerPage()
	{
		InitializeComponent();
	}

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}