namespace Projekt_Siłownia;

public partial class Progress : ContentPage
{
	public Progress()
	{
		InitializeComponent();
	}

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new UserPage();
    }
}