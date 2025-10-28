using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

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

        string[] dates = chartData.Select(x => x.TreningDate.ToString("yyyy-MM-dd")).ToArray();
        double[] scores = chartData.Select(x => x.TotalScore).ToArray();

        var series = new ISeries[]
        {
            new ColumnSeries<double>
            {
                Name = "Wynik",
                Values = scores,
                Fill = new SolidColorPaint(SKColors.DeepSkyBlue)
            }
        };

        ScoreChart.Series = series;

        ScoreChart.XAxes = new Axis[]
        {
            new Axis
            {
                Labels = dates,
                LabelsRotation = 0,
                Name = "Data"
            }
        };

        ScoreChart.YAxes = new Axis[]
        {
            new Axis
            {
                Name = "Łączny wynik"
            }
        };
    }



}