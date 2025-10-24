using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class UsersKlientCarnet
{
    public int UsersKlientCarnetId { get; set; }

    public int UsersKlientId { get; set; }

    public int CarnetId { get; set; }

    public DateOnly CarnetStartdate { get; set; }

    public DateOnly CarnetEnddate { get; set; }
}
