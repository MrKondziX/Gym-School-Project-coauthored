using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlientTreningday
{
    public int UsersTreningdayId { get; set; }

    public int UsersKlientId { get; set; }

    public DateOnly UsersTreningdayDate { get; set; }

    public string UsersTreningdayTime { get; set; } = null!;
}
