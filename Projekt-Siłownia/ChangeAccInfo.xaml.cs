using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Projekt_Siłownia;

public partial class ChangeAccInfo : ContentPage, INotifyPropertyChanged
{
    // chuj nie chce mi sie tego robić dla użytkownika, zrobię to dla trenera, a potem się zobaczy czy będzie sens robić to dla użytkownika
    // jakby do UserPage szedł normalnie userId, to bym to zrobił wspólne, ale teraz idzie tam usersKlientId to nie chce mi sie kombinować z wydobywaniem usersId z usersKlientId
    // chyba że JONATAN to przerobi tak żeby to działało i dla trenera i dla użytkownika, to będzie wspólne
    // na razie robię to tylko dla trenera



    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private int UserId { get; set; }
    private string userName;
    public string UserNameM
    {
        get => userName;
        set
        {
            userName = value;
            OnPropertyChanged();
        }
    }
    private string userSurname;
    public string UserSurnameM
    {
        get => userSurname;
        set
        {
            userSurname = value;
            OnPropertyChanged();
        }
    }
    private string userEmail;
    public string UserEmailM
    {
        get => userEmail;
        set
        {
            userEmail = value;
            OnPropertyChanged();
        }
    }
    private string password;
    public string PasswordM
    {
        get => password;
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }
    private string checkPassword;
    public string CheckPasswordM
    {
        get => checkPassword;
        set
        {
            checkPassword = value;
            OnPropertyChanged();
        }
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return computedHash;
    }
    private User modifiedUser;
    public ChangeAccInfo(int userId)
    {
        InitializeComponent();
        this.BindingContext = this;
        this.UserId = userId;
        LoadUserData();
    }

    private void LoadUserData()
    {
        //throw new NotImplementedException();
        using var context = new GymAppDbContext();
        modifiedUser = context.Users.FirstOrDefault(u => u.UsersId == UserId);
        if (modifiedUser != null)
        {
            UserNameM = modifiedUser.UsersName;
            UserSurnameM = modifiedUser.UsersSurname;
            UserEmailM = modifiedUser.UsersEmail;
        }
    }
    private async void SaveChangesButtonClicked(object sender, EventArgs e)
    {
        var q = await DisplayAlert("Pytanie", "Czy na pewno chcesz zapisać zmiany?", "Tak", "Nie");
        if (!q)
        {
            await DisplayAlert("Anulowano", "Zmiany nie zostały zapisane", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(UserNameM) ||
                string.IsNullOrWhiteSpace(UserSurnameM) ||
                string.IsNullOrWhiteSpace(UserEmailM))
            {
                await DisplayAlert("Błąd", "Wypełnij Wszystkie Pola", "OK");
                return;
            }
            if (!string.IsNullOrWhiteSpace(PasswordM) || !string.IsNullOrWhiteSpace(CheckPasswordM))
            {
                if (PasswordM != CheckPasswordM)
                {
                    await DisplayAlert("Błąd", "Hasła nie są identyczne", "OK");
                    return;
                }
                modifiedUser.UsersPassword = HashPassword(PasswordM);
            }
            modifiedUser.UsersName = UserNameM;
            modifiedUser.UsersSurname = UserSurnameM;
            modifiedUser.UsersEmail = UserEmailM;
            using var context = new GymAppDbContext();
            context.Users.Update(modifiedUser);
            await context.SaveChangesAsync();
            await DisplayAlert("Sukces", "Dane zostały zaktualizowane", "OK");
            Application.Current.MainPage = new NavigationPage(new TrainerPage((int)UserId));

        }
        
    }
    private async void CancelButtonClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Anulowano", "Operacja anulowana", "OK");
        Application.Current.MainPage = new NavigationPage(new TrainerPage((int)UserId));
    }
}
