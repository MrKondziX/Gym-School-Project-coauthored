using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Projekt_Siłownia;

public partial class TrainerPage : ContentPage, INotifyPropertyChanged
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
    private int? UserId { get; set; }
    public TrainerPage(int userId)
	{
		InitializeComponent();
 
        Users = new ObservableCollection<User>();
        this.BindingContext = this;
        this.UserId = userId;
        InsertData();
    }
    private void InsertData()
    {
        using var context = new GymAppDbContext();
        var trainer = context.Users.FirstOrDefault(u => u.UsersId == UserId);
        if (trainer != null)
        {
            TrainerName = $"{trainer.UsersName} {trainer.UsersSurname}";
        }

        var result = context.Users.Where(u => context.UsersKlients.Any(uk => uk.UsersId == u.UsersId && context.UsersCoaches.Any(c => c.UsersCoachId == uk.UsersCoachId && c.UsersId == UserId))).ToList();
        Users.Clear();
        foreach (var user in result)
        {
            Users.Add(user);
        }
    }

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }


    private async void AssignWorkout_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new AssignWorkout((int)UserId));
    }

    private void ChangeAccInfo_Clicked(object sender, EventArgs e)
    {
        //póki co nic nie robi
        Debug.WriteLine("ChangeAccInfo Clicked");
    }
}