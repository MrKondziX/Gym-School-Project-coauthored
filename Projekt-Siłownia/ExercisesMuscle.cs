using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class ExercisesMuscle
{
    public int ExsMuscleId { get; set; }

    public string ExsMuscleName { get; set; } = null!;

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
}
