using System.Security.Cryptography;
using System.Text;

namespace Projekt_Siłownia;

public partial class RegisterPage : ContentPage
{
    private readonly AuthService _authService = new();
    public RegisterPage()
	{
		InitializeComponent();
	}

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHash = BitConverter.ToString(bytes).Replace("-","").ToLower();
        return computedHash;
    }
    private async void RegisterButtonClicked(object sender, EventArgs e)
	{
		string name = NameEntry.Text;
		string surname = SurnameEntry.Text;
		string login = LoginEntry.Text;
		string email = EmailEntry.Text;
		string password = PassEntry.Text;
		string repeatpassword = RepeatPassEntry.Text;

		if (string.IsNullOrWhiteSpace(name) )
		{
			await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
			return;
		}
		if (string.IsNullOrWhiteSpace(surname))
		{
            await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(login))
        {
            await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
            return;
        }
        if (string.IsNullOrWhiteSpace(repeatpassword))
        {
            await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
            return;
        }
        if (password != repeatpassword)
		{
			await DisplayAlert("Błąd", "Hasła Się Nie Zgadzają", "OK");
			return;
		}

		string hassedPass = HashPassword(password);

		string result = await _authService.Register(name, surname, email, login, hassedPass);
		await DisplayAlert("Rejestracja", result, "OK");

        NameEntry.Text = string.Empty;
        SurnameEntry.Text = string.Empty;
        LoginEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PassEntry.Text = string.Empty;
        RepeatPassEntry.Text = string.Empty;
    }

    /*Przejście Do Strony Zaloguj Się*/
    private async void AlreadyHaveAnAccount(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}