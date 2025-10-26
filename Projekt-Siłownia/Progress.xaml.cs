using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Projekt_Siłownia;

public partial class Progress : ContentPage
{

    public int UserId { get; set; }

	public Progress(int userId)
	{
        this.UserId = userId;
        InitializeComponent();
        SetUpChart();
	}

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new UserPage(UserId);
    }



    private void SetUpChart()
    {
        using var db = new GymAppDbContext();

        var chartData = (
            from t in db.UsersKlientTrenings
            where t.UsersKlientId == UserId
            group t by t.UsersKlientTreningDate into g
            select new
            {
                TreningDate = g.Key,
                TotalScore = g.Sum(x => (double)x.TreningWeight * x.TreningSeries)
            }
        )
        .OrderBy(x => x.TreningDate)
        .ToList();

        foreach (var item in chartData)
        {
            Debug.WriteLine($"Date: {item.TreningDate}, Total Score: {item.TotalScore}");
        }
    }



}