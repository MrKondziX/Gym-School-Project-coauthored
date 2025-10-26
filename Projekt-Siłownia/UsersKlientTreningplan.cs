using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlientTreningplan
{
    public int TreningplanId { get; set; }

    public int UsersKlientId { get; set; }

    public int ExsId { get; set; }

    public string TreningplanNote { get; set; } = null!;
    public int TreningDayWeek { get; set; }

    public DateOnly TreningplanDate { get; set; }
}
