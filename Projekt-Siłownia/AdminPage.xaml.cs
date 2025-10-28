using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Projekt_Siłownia;

public partial class AdminPage : ContentPage , INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private ObservableCollection<User> users;
    public ObservableCollection<User> Users
    {
        get => users;
        set
        {
            users = value;
            OnPropertyChanged();
        }
    }
    private string trainerName;
    public string TrainerName
    {
        get => trainerName;
        set
        {
            trainerName = value;
            OnPropertyChanged();
        }
    }
    private readonly AuthService _authService = new();
    public AdminPage()
	{
		InitializeComponent();

        Users = new ObservableCollection<User>();
        this.BindingContext = this;
        InsertData();
    }

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        var computedHash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return computedHash;
    }
    private async void AddCoachButtonClicked(object sender, EventArgs e)
    {
        string name = NameEntry.Text;
        string surname = SurnameEntry.Text;
        string login = LoginEntry.Text;
        string email = EmailEntry.Text;
        string password = PassEntry.Text;
        string repeatpassword = RepeatPassEntry.Text;

        if (string.IsNullOrWhiteSpace(name))
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

        string result = await _authService.AddCoach(name, surname, email, login, hassedPass);
        await DisplayAlert("Rejestracja", result, "OK");

        NameEntry.Text = string.Empty;
        SurnameEntry.Text = string.Empty;
        LoginEntry.Text = string.Empty;
        EmailEntry.Text = string.Empty;
        PassEntry.Text = string.Empty;
        RepeatPassEntry.Text = string.Empty;
    }
    private async Task ShowOnlyAsync(VisualElement layoutToShow)
    {
        // Znajdź wszystkie layouty na stronie
        var layouts = new[] { layout1, layout2, layout3 , LayoutCoachAdd };

        // Ukryj te, które są inne
        foreach (var layout in layouts)
        {
            if (layout == layoutToShow)
                continue;

            if (layout.IsVisible)
            {
                await layout.FadeTo(0, 200); // 200ms
                layout.IsVisible = false;
            }
        }

        // Pokaż wybrany layout
        if (!layoutToShow.IsVisible)
        {
            layoutToShow.Opacity = 0;
            layoutToShow.IsVisible = true;
            await layoutToShow.FadeTo(1, 200);
        }
    }

    private async void OnLayout1Clicked(object sender, EventArgs e)
    { 
        await ShowOnlyAsync(layout1);
        InsertData();
    }
    private async void OnLayout2Clicked(object sender, EventArgs e)
    {
        await ShowOnlyAsync(layout2);
        InsertDataCoach();
    }

    private async void OnLayout3Clicked(object sender, EventArgs e)
        => await ShowOnlyAsync(layout3);
    private async void OnLayoutCouchAddClicked(object sender, EventArgs e)
        => await ShowOnlyAsync(LayoutCoachAdd);

    private void InsertDataCoach()
    {
        using var context = new GymAppDbContext();

        var result = context.Users.Where(u => u.UsersTypeId == 2).ToList();
        Users.Clear();
        foreach (var user in result)
        {
            Users.Add(user);
        }
    }
    private void InsertData()
    {
        using var context = new GymAppDbContext();

        var result = context.Users.ToList();
        Users.Clear();
        foreach (var user in result)
        {
            Users.Add(user);
        }
    }
}

