using System;
using System.Collections.Generic;

#nullable disable

namespace PremiumDental.Models
{
    public partial class Karton
    {
        public int Id { get; set; }
        public string BrojKartona { get; set; }
        public string ZnakUpozorenja { get; set; }
        public string Status { get; set; }
        public string Html { get; set; }
    }
}
