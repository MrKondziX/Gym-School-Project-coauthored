using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlientTrening
{
    public int TreningId { get; set; }

    public int UsersKlientId { get; set; }

    public DateOnly UsersKlientTreningDate { get; set; }

    public int TreningWeight { get; set; }

    public int TreningSeries { get; set; }

    public int ExsId { get; set; }

    public virtual Exercise Exs { get; set; } = null!;

    public virtual UsersKlient UsersKlient { get; set; } = null!;
}
