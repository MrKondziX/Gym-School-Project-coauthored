using System.Net.Http.Json;

namespace Projekt_Siłownia
{
    public partial class LoginPage : ContentPage
    {
       private readonly AuthService _authService = new();

        public LoginPage()
        {
            InitializeComponent();
        }

        /*Przejście Do Strony Zarejestruj Się*/
        private async void ZarejestrujSie_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            string login = LoginEntry.Text?.Trim();
            string password = PassEntry.Text;


            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Błąd", "Podaj Nazwę Użytkownika I Hasło", "OK");
                return;
            }

            var (result, userType) = await _authService.Login(login, password);

            if (result == "Zalogowano Pomyślnie")
            {
                if (userType == 1)
                {
                    await DisplayAlert("Sukces", "Zalogowano Pomyślnie", "OK");
                    Window.Page = new AdminPage();
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                }
                else if (userType == 2)
                {
                    await DisplayAlert("Sukces", "Zalogowano Pomyślnie", "OK");
                    Window.Page = new TrainerPage();
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                }
                else if (userType == 3)
                {
                    await DisplayAlert("Sukces", "Zalogowano Pomyślnie", "OK");
                    Window.Page = new UserPage();
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                }
            }
            else
            {
                await DisplayAlert("Błąd", result, "OK");
            }
        }
    }

}
