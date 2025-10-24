namespace Projekt_Siłownia;

public partial class AssignWorkout : ContentPage
{
    private int? UserId { get; set; }
    public AssignWorkout(int userId)
	{
		InitializeComponent();
        this.BindingContext = this;
        this.UserId = userId;
    }

    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TrainerPage((int)UserId)); //potem zmienić na właściwe przekazywanie UserId, na razie jedynie jak to widzę to userId będzie musiało chodzić za stronami panelu Trenera, ale póki co jest tak żeby nie było błędów
    }

    private async void Assign_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Workout());
    }
}