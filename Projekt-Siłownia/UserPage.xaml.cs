using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Diagnostics;



namespace Projekt_Siłownia;

public partial class UserPage : ContentPage
{
    //ObservableCollection
    GymAppDbContext context = new GymAppDbContext();
    public int UserId { get; set; }
    public UserPage(int userId)
    {
        InitializeComponent();

        this.UserId = userId;
        LoadTreningi();
        WczytajKarnet();


    }
    private void WczytajKarnet()
    {
        Debug.WriteLine(UserId);
        var dzisiaj = DateOnly.FromDateTime(DateTime.Today);
        var aktywny = context.UsersKlientCarnets
            .Where(ukc => ukc.UsersKlientId == UserId && ukc.CarnetEnddate >= dzisiaj)
            .OrderByDescending(ukc => ukc.UsersKlientCarnetId)
            .FirstOrDefault();


        if (aktywny != null)
        {
            var karnet = context.Carnets
                .Where(c => c.CarnetId == aktywny.CarnetId)
                .OrderByDescending(c => c.CarnetId)
                .FirstOrDefault();


            if (karnet != null)
            {
                Rodzaj.Text = $"Rodzaj: {karnet.CarnetName}";
                Data.Text = $"Ważny od: {aktywny.CarnetStartdate:dd.MM.yyyy} do {aktywny.CarnetEnddate:dd.MM.yyyy}";
            }
            else
            {
                Rodzaj.Text = "Nie znaleziono danych karnetu";
                Data.Text = "";
            }
        }
        else
        {
            Rodzaj.Text = "Brak aktywnego karnetu";
            Data.Text = "";
        }
    }

    private void LoadTreningi()
    {

        var treningi = context.UsersKlientTreningplan
            .Where(tp => tp.UsersKlientId == UserId)
            .Select(tp => tp.TreningDayWeek)
            .Distinct()
            .Select(day => new TreningOption { DayValue = day })
            .ToList();


        TreningPicker.ItemsSource = treningi;
        TreningPicker.SelectedIndex = 0;

    }
    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());

    }

    private void OdnowButton_Clicked(object sender, EventArgs e)
    {
        Window.Page = new KupKarnet((int)UserId);
    }

    private void Start_Clicked(object sender, EventArgs e)
    {
        var selected = TreningPicker.SelectedItem as TreningOption;
        Debug.WriteLine(selected.DayValue);
        Window.Page = new Workout((int)selected.DayValue, (int)UserId);
    }
    //Placeholder by MJ
    private void Progress_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new Progress(UserId));
    }

}