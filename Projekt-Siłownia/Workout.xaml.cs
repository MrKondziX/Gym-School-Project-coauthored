namespace Projekt_Siłownia;

public partial class Workout : ContentPage
{
    public Workout()
    {
        InitializeComponent();
    }

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    private void Button_Clicked(object sender, EventArgs e)
    {

    }

    //Placeholder by MJ
    private void Progress_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Progress());
    }
}
