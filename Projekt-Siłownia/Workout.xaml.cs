using Microsoft.EntityFrameworkCore;

namespace Projekt_Siłownia;

public partial class Workout : ContentPage
{
    public int TreningId;
    public int ExerciseIndex = 0;
    public int curSeries = 0;
    public int Series = 3; 
    public List<Exercies_View> Exercises;
    GymAppDbContext context = new GymAppDbContext();
    public int UserId { get; set; }
    public Workout(int treningId, int userId)
    {
        this.TreningId = treningId;
        this.UserId = userId;
        InitializeComponent();
        LoadExercises();
    }
    private async Task LoadExercises()
    {
        var Exercises = await (
            from plan in context.UsersKlientTreningplans
            where plan.UsersKlientId == UserId
            join ex in context.Exercises on plan.ExsId equals ex.ExsId into exJoin
            from ex in exJoin.DefaultIfEmpty()
            join muscle in context.ExercisesMuscles on ex.ExsMuscleId equals muscle.ExsMuscleId into mJoin
            from muscle in mJoin.DefaultIfEmpty()
            select new Exercies_View
            {
                ExsId = ex.ExsId,
                ExsName = ex.ExsName,
                ExsDescription = ex.ExsDescription,
                MuscleGroup = muscle.ExsMuscleName,
                ExsNote = plan.TreningplanNote
            }).ToListAsync();




        DisplayCurrentExercise();
    }
    private void DisplayCurrentExercise()
    {
        if (ExerciseIndex <= Exercises.Count)
        {
            var exercise = Exercises[ExerciseIndex];
            exerciseLabel.Text = exercise.ExsName;
            muscleLabel.Text = $"Partia mięśniowa: {exercise.MuscleGroup}";
            weightEntry.Text = "";
            repsEntry.Text = "";
            rpeEntry.Text = "";
            curSeries = 0;
        }
        else
        {
            DisplayAlert("Koniec", "Wszystkie ćwiczenia zakończone!", "OK");
            Navigation.PopAsync();
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (!int.TryParse(weightEntry.Text, out int weight) ||
            !int.TryParse(repsEntry.Text, out int reps) ||
            !int.TryParse(rpeEntry.Text, out int rpe))
        {
            await DisplayAlert("Błąd", "Wprowadź poprawne dane liczbowe", "OK");
            return;
        }

        var currentExercise = Exercises[ExerciseIndex];

        var treningRecord = new UsersKlientTrening
        {
            UsersKlientId = UserId,
            TreningWeight = weight,
            TreningSeries = curSeries + 1,
            UsersTreningdayId = (int)DateTime.Now.DayOfWeek
        };

        context.UsersKlientTrenings.Add(treningRecord);
        await context.SaveChangesAsync();

        curSeries++;

        if (curSeries >= Series)
        {
            ExerciseIndex++;
            DisplayCurrentExercise();
        }
        else
        {
            await DisplayAlert("Seria", $"Zapisano serię {curSeries}/{Series}", "OK");
        }
    }

    private void IncreaseSeries(object sender, EventArgs e)
    {
        Series++;
        seriesLabel.Text = $"Serie: {Series}";
    }

    private void DecreaseSeries(object sender, EventArgs e)
    {
        if (Series > 1)
        {
            Series--;
            seriesLabel.Text = $"Serie: {Series}";
        }
    }
    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }



}
