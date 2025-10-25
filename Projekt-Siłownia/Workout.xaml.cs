namespace Projekt_Siłownia;

public partial class Workout : ContentPage
{
    public Workout()
    {
        InitializeComponent();
    }

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}
