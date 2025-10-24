using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class Exercise
{
    public int ExsId { get; set; }

    public int ExsMuscleId { get; set; }

    public string ExsName { get; set; } = null!;

    public string ExsDescription { get; set; } = null!;
}
