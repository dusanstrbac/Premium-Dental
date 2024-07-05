using System;
using System.Collections.Generic;

#nullable disable

namespace PremiumDental.Models
{
    public partial class Zakazivanje
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime? VremeOd { get; set; }
        public DateTime? VremeDo { get; set; }
        public int? Doktor { get; set; }
        public string Usluga { get; set; }
        public string Napomena { get; set; }
        public string Docek { get; set; }
        public string Smestaj { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Expr1 { get; set; }
        public string Expr2 { get; set; }
        public string Boja { get; set; }
    }
}
