using System.Collections.ObjectModel;

namespace Projekt_Siłownia;

public partial class UserPage : ContentPage
{
    //ObservableCollection
	public UserPage()
	{
		InitializeComponent();
	}

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    private void OdnowButton_Clicked(object sender, EventArgs e)
    {

    }

    //Placeholder by MJ
    private void Progress_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new Progress();
    }
}