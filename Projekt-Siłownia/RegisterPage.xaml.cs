namespace Projekt_Siłownia;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    /*Przejście Do Strony Zaloguj Się*/
    private async void AlreadyHaveAnAccount(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}