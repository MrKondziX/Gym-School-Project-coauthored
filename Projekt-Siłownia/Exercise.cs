using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class Exercise
{
    public int ExsId { get; set; }

    public int ExsMuscleId { get; set; }

    public string ExsName { get; set; } = null!;

    public string ExsDescription { get; set; } = null!;

    public virtual ExercisesMuscle ExsMuscle { get; set; } = null!;

    public virtual ICollection<UsersKlientTrening> UsersKlientTrenings { get; set; } = new List<UsersKlientTrening>();
}
