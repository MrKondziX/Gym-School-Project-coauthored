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


            if(string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Błąd", "Podaj Nazwę Użytkownika I Hasło", "OK");
                return;
            }
            string result = await _authService.Login(login, password);

            if(result == "Zalogowano Pomyślnie")
            {
                await DisplayAlert("Sukces", "Zalogowano Pomyślnie", "OK");
                Window.Page = new Workout();

                LoginEntry.Text = string.Empty;
                PassEntry.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("BŁĄD", result, "OK");
            }
        }
    }

}
