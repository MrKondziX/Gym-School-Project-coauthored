namespace Projekt_Siłownia;

public partial class Progress : ContentPage
{

    public int UserId { get; set; }

	public Progress(int userId)
	{
		InitializeComponent();
        this.UserId = userId;
	}

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new UserPage(UserId);
    }
}