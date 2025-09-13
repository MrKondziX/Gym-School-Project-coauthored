namespace Projekt_Siłownia
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        /*Przejście Do Strony Zarejestruj Się*/
        private async void ZarejestrujSie_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }
    }

}
