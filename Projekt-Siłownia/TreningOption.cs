using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Siłownia
{
    public class TreningOption
    {
        public int DayValue { get; set; }
        public string DisplayText => $"Trening {DayValue}";
    }
}
