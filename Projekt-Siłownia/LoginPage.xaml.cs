using System.Diagnostics;
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

            var (result, userType, userId) = await _authService.Login(login, password);

            if (result == "Zalogowano Pomyślnie")
            {
                if (userType == 1)
                {
                    Window.Page = new AdminPage();
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                    
                }
                else if (userType == 2)
                {
                    Window.Page = new TrainerPage((int)userId);
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                    
                }
                else if (userType == 3)
                {
                    using var db = new GymAppDbContext();
                    int userId2 = db.UsersKlients
                        .Where(uk => uk.UsersId == userId)
                        .Select(uk => uk.UsersKlientId)
                        .FirstOrDefault();
                    Window.Page = new UserPage((int)userId2);
                    LoginEntry.Text = string.Empty;
                    PassEntry.Text = string.Empty;
                  //  Debug.WriteLine(userId); ---> zostawiam zakomentowane na wszelki wypadek, może sie przydać do debugowania
                }
            }
            else
            {
                await DisplayAlert("Błąd", result, "OK");
            }
        }
    }

}
