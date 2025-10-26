namespace Projekt_Siłownia;

public partial class KupKarnet : ContentPage
{
    GymAppDbContext context = new GymAppDbContext();
    public int UserId { get; set; }
    public List<int> DniKarnetu = new List<int> { 30, 90, 360, 30, 90, 360};
    public KupKarnet(int userId)
    {
        this.UserId = userId;
        InitializeComponent();
        WczytajKarnety();
    }
    private void WczytajKarnety()
    {
        var karnety = context.Carnets.ToList();
        CarnetPicker.ItemsSource = karnety;
    }

    private void CarnetPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var wybrany = (Carnet)CarnetPicker.SelectedItem;
        if (wybrany != null)
        {
            CzasTrwaniaLabel.Text = $"{wybrany.CarnetName}\nCena: {wybrany.CarnetCost} zł";
        }
        else
        {
            CzasTrwaniaLabel.Text = "";
        }
    }

    private async void KupKarnet_Clicked(object sender, EventArgs e)
    {
        var wybrany = (Carnet)CarnetPicker.SelectedItem;
        if (wybrany == null)
        {
            await DisplayAlert("Błąd", "Wybierz typ karnetu", "OK");
            return;
        }

        var nowy = new UsersKlientCarnet
        {
            UsersKlientId = UserId,
            CarnetId = wybrany.CarnetId,
            CarnetStartdate = DateOnly.FromDateTime(DateTime.Today),
            CarnetEnddate = DateOnly.FromDateTime(DateTime.Today.AddDays(DniKarnetu[wybrany.CarnetId - 1]))
        };

        context.UsersKlientCarnets.Add(nowy);
        context.SaveChanges();

        await DisplayAlert("Sukces", $"Zakupiono: {wybrany.CarnetName}", "OK");
        Window.Page = new UserPage((int)UserId);
    }
}