using System;
using System.Collections.Generic;

#nullable disable

namespace PremiumDental.Models
{
    public partial class Doktori
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Boja { get; set; }
    }
}
