using System;
using System.Collections.Generic;

namespace Projekt_Siłownia;

public partial class Carnet
{
    public int CarnetId { get; set; }

    public string CarnetName { get; set; } = null!;

    public double CarnetCost { get; set; }
}
