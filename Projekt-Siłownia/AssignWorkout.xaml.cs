using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Projekt_Siłownia;

public partial class AssignWorkout : ContentPage, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private int? UserId { get; set; }
    private User SelectedUser { get; set; }
    private string usersName;
    public string UsersName { get => usersName; set { usersName = value; OnPropertyChanged(); } }

    private ObservableCollection<ExercisesMuscle> exercisesMuscles;
    public ObservableCollection<ExercisesMuscle> ExercisesMuscles
    {
        get => exercisesMuscles;
        set
        {
            exercisesMuscles = value;
            OnPropertyChanged();
        }
    }
    private ExercisesMuscle selectedMuscle;
    public ExercisesMuscle SelectedMuscle { get => selectedMuscle; set { selectedMuscle = value; OnPropertyChanged(); SelectedMuscleId = value?.ExsMuscleId ?? 0; } }
    private int selectedMuscleId;
    public int SelectedMuscleId { get => selectedMuscleId; set { selectedMuscleId = value; OnPropertyChanged(); } }
    private ObservableCollection<Exercise> exercises;
    public ObservableCollection<Exercise> Exercises { get => exercises; set { exercises = value; OnPropertyChanged(); } }
    private Exercise selectedExercise;
    public Exercise SelectedExercise { get => selectedExercise; set { selectedExercise = value; OnPropertyChanged(); SelectedExerciseId = value?.ExsId ?? 0; } }
    private int selectedExerciseId;
    public int SelectedExerciseId { get => selectedExerciseId; set { selectedExerciseId = value; OnPropertyChanged(); } }
    private string comment;
    public string Comment { get => comment; set { comment = value; OnPropertyChanged(); } }
    public DateTime MinDate { get; set; }
    public DateTime MaxDate { get; set; }
    private DateTime workoutDate;
    public DateTime WorkoutDate { get => workoutDate; set { workoutDate = value; OnPropertyChanged(); } }
    private int clientId;

    public AssignWorkout(int userId, User selectedUser)
	{
        MinDate = DateTime.Now.Date;
        MaxDate = MinDate.AddYears(1);
        WorkoutDate = MinDate;
        InitializeComponent();
        this.BindingContext = this;
        this.UserId = userId;
        this.SelectedUser = selectedUser;
        ExercisesMuscles = new ObservableCollection<ExercisesMuscle>();
        Exercises = new ObservableCollection<Exercise>();

        //Debug.WriteLine($"TEST2 {SelectedUser.UsersId}");
        LoadData();
    }

    private void LoadData()
    {
        using var db = new GymAppDbContext();
        var selectedUserInfo = db.Users.FirstOrDefault(u => u.UsersId == SelectedUser.UsersId);
        if (selectedUserInfo != null)
        {
            UsersName = $"{selectedUserInfo.UsersName} {selectedUserInfo.UsersSurname}";
            // throw new NotImplementedException();
        }
        var musclesFromDb = db.ExercisesMuscles.ToList();
        ExercisesMuscles.Clear();
        foreach (var muscle in musclesFromDb)
        {
            ExercisesMuscles.Add(muscle);
        }
        var clientIdFromDb = db.UsersKlients.FirstOrDefault(uk => uk.UsersId == SelectedUser.UsersId);
        clientId = clientIdFromDb.UsersKlientId;
    }

    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TrainerPage((int)UserId)); //na razie jedynie jak to widze to id trenera chodzi za stronami trenera
    }

    private async void Assign_Clicked(object sender, EventArgs e)
    {
        bool q = await DisplayAlert("Pytanie", "Czy na pewno chcesz dodać plan ćwiczeniowy dla tego użtkownika?", "Tak", "Nie");
        if (q)
        {
            var datetoDb = DateOnly.FromDateTime(WorkoutDate);
            var newWorkoutPlan = new UsersKlientTreningplan
            {
                TreningplanId = 0,
                UsersKlientId = clientId,
                ExsId = SelectedExerciseId,
                TreningplanNote = Comment,
                TreningplanDate = datetoDb,
                TreningDayWeek = (int)datetoDb.DayOfWeek
            };
            using var db = new GymAppDbContext();
            db.UsersKlientTreningplan.Add(newWorkoutPlan);
            await db.SaveChangesAsync();
            await DisplayAlert("Informacja", "Dodano", "OK");
            GoBack_Clicked(sender, e);
        }
        else
        {
            await DisplayAlert("Anulowano", "Dodawanie planu treningowego zostało anulowane", "OK");
            GoBack_Clicked(sender, e);
        }
    }

    private void MusclePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        using var db = new GymAppDbContext();
        var exercisesFromDb = db.Exercises.Where(e => e.ExsMuscleId == SelectedMuscleId);
        Exercises.Clear();
        foreach(var exercise in exercisesFromDb)
        {
            Exercises.Add(exercise);
        }
    }
}