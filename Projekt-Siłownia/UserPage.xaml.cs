namespace Projekt_Siłownia;

public partial class UserPage : ContentPage
{
	public UserPage()
	{
		InitializeComponent();
	}

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}