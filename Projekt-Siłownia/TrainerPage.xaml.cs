namespace Projekt_Siłownia;

public partial class TrainerPage : ContentPage
{
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
    private int? UserId { get; set; }
    public TrainerPage(int userId)
	{
		InitializeComponent();
        this.BindingContext = this;
        this.UserId = userId;
        RefreshTrainerName();
    }
    private void RefreshTrainerName()
    {
        using var context = new GymAppDbContext();
        var trainer = context.Users.FirstOrDefault(u => u.UsersId == UserId);
        if (trainer != null)
        {
            TrainerName = $"{trainer.UsersName} {trainer.UsersSurname}";
        }
    }

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}