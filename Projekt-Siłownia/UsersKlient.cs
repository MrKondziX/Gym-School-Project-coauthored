using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlient
{
    public int UsersKlientId { get; set; }

    public int UsersId { get; set; }

    public int UsersCoachId { get; set; }

    public virtual User Users { get; set; } = null!;

    public virtual UsersCoach UsersCoach { get; set; } = null!;

    public virtual ICollection<UsersKlientCarnet> UsersKlientCarnets { get; set; } = new List<UsersKlientCarnet>();

    public virtual ICollection<UsersKlientTreningplan> UsersKlientTreningplans { get; set; } = new List<UsersKlientTreningplan>();

    public virtual ICollection<UsersKlientTrening> UsersKlientTrenings { get; set; } = new List<UsersKlientTrening>();
}
