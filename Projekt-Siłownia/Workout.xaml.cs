using Microsoft.EntityFrameworkCore;

namespace Projekt_Siłownia;

public partial class Workout : ContentPage
{
    public int TreningId;
    public int ExerciseIndex = 0;
    public int curSeries = 0;
    public int Series = 3;
    public int TreningDayId;
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
        var treningDay = new UsersKlientTreningday
        {
            UsersKlientId = UserId,
           

            UsersTreningdayDate = DateOnly.FromDateTime(DateTime.Today),
            UsersTreningdayTime = DateTime.Today.Add(TimeOnly.FromDateTime(DateTime.Now).ToTimeSpan()).ToString()

        };


        context.UsersKlientTreningdays.Add(treningDay);
        await context.SaveChangesAsync();

        this.TreningDayId = context.UsersKlientTreningdays
          .Where(c => c.UsersKlientId == UserId)
          .OrderByDescending(c => c.UsersTreningdayId)
          .Select(c => c.UsersTreningdayId)
          .FirstOrDefault();
        Exercises = await (
            from plan in context.UsersKlientTreningplan
            where plan.UsersKlientId == UserId
               && plan.TreningDayWeek == TreningId 
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
        if(Exercises is null)
        {
            DisplayAlert("Brak ćwiczeń", "Brak ćwiczeń do wyświetlenia", "OK");
            Application.Current.MainPage = new UserPage(UserId);
            return;
        }

        if (ExerciseIndex < Exercises.Count)
        {
            var exercise = Exercises[ExerciseIndex];
            exerciseLabel.Text = exercise.ExsName;
            muscleLabel.Text = $"Partia mięśniowa: {exercise.MuscleGroup}";
            noteLabel.Text = $"Notatka: {exercise.ExsNote}";
            weightEntry.Text = "";
            repsEntry.Text = "";
            rpeEntry.Text = "";
            curSeries = 0;
        }
        else
        {
            DisplayAlert("Koniec", "Wszystkie ćwiczenia zakończone!", "OK");
            Application.Current.MainPage = new UserPage((int)UserId);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (!float.TryParse(weightEntry.Text, out float weight) ||
            !float.TryParse(repsEntry.Text, out float reps) ||
            !float.TryParse(rpeEntry.Text, out float rpe))
        {
            await DisplayAlert("Błąd", "Wprowadź poprawne dane liczbowe", "OK");
            return;
        }

        if(weight <= 0)
        {
            weight = 1;
        }

        var currentExercise = Exercises[ExerciseIndex];

        var treningRecord = new UsersKlientTrening
        {
            UsersKlientId = UserId,
            TreningWeight = weight,
            TreningSeries = curSeries + 1,
            ExsId = Exercises[ExerciseIndex].ExsId,
            UsersKlientTreningDate = DateOnly.FromDateTime(DateTime.Today),
            UsersTreningdayId = TreningDayId,
            Powtorzenia = reps,
            rpe = rpe
        };


        context.UsersKlientTrenings.Add(treningRecord);
        await context.SaveChangesAsync();
        context.Entry(treningRecord).State = EntityState.Detached;

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

    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new UserPage((int)UserId);
    }
    
}

