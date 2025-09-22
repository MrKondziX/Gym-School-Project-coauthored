using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersCoach
{
    public int UsersCoachId { get; set; }

    public int UsersId { get; set; }

    public string UsersCoachNott { get; set; } = null!;

    public virtual User Users { get; set; } = null!;

    public virtual ICollection<UsersKlient> UsersKlients { get; set; } = new List<UsersKlient>();
}
