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
                TotalScore = g.Sum(x => (double)x.TreningWeight * x.Powtorzenia)
            }
        )
        .OrderBy(x => x.TreningDate)
        .ToList();

        string[] dates = chartData.Select(x => x.TreningDate.ToString("yyyy-MM-dd")).ToArray();
        double[] scores = chartData.Select(x => x.TotalScore).ToArray();

        var columnSeries = new ColumnSeries<double>
        {
            Name = "Wynik",
            Values = scores,
            Fill = new SolidColorPaint(SKColor.Parse("#ff66a5"))
        };

        columnSeries.ChartPointPointerDown += (point, index) =>
        {
            DateOnly clickedDate = chartData[index.Index].TreningDate;

            ShowTrainingDetailsForDate(clickedDate);
        };

        ScoreChart.Series = new ISeries[] { columnSeries };

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


    private void ShowTrainingDetailsForDate(DateOnly date)
    {
        using var db = new GymAppDbContext();

        var trainings = (
            from t in db.UsersKlientTrenings
            join e in db.Exercises on t.ExsId equals e.ExsId
            where t.UsersKlientId == UserId
                  && t.UsersKlientTreningDate == date
            group t by new { t.TreningWeight, t.Powtorzenia, e.ExsName } into g
            select new
            {
                g.Key.ExsName,
                g.Key.TreningWeight,
                g.Key.Powtorzenia,
                Count = g.Count(),
            }
        )
        .OrderBy(t => t.ExsName)
        .ThenBy(t => t.TreningWeight)
        .ToList();

        ExercisesStack.Children.Clear();

        if (!trainings.Any())
        {
            ExercisesStack.Children.Add(new Label
            {
                Text = $"Brak treningów dla {date:yyyy-MM-dd}",
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Gray
            });
            return;
        }

        ExercisesStack.Children.Add(new Label
        {
            Text = $"Treningi z dnia {date:yyyy-MM-dd}:",
            FontAttributes = FontAttributes.Bold,
            FontSize = 16
        });

        foreach (var t in trainings)
        {
            ExercisesStack.Children.Add(new Label
            {
                Text = $"• {t.ExsName}: {t.TreningWeight} kg × {t.Powtorzenia} powtórzeń × serie {t.Count}",
                FontSize = 14
            });
        }
    }

}