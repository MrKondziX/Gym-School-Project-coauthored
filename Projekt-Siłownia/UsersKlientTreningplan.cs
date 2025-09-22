using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlientTreningplan
{
    public int TreningplanId { get; set; }

    public int UsersKlientId { get; set; }

    public string Monday { get; set; } = null!;

    public string Tuesday { get; set; } = null!;

    public string Wendsday { get; set; } = null!;

    public string Thursday { get; set; } = null!;

    public string Friday { get; set; } = null!;

    public string Saturday { get; set; } = null!;

    public string Sunday { get; set; } = null!;

    public virtual UsersKlient UsersKlient { get; set; } = null!;
}
