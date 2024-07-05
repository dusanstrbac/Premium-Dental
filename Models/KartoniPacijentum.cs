using System;
using System.Collections.Generic;

#nullable disable

namespace PremiumDental.Models
{
    public partial class KartoniPacijentum
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }
        public string Pol { get; set; }
        public string Jmbg { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string BrojKartona { get; set; }
        public string DatumRodjenja { get; set; }
        public string LicniDokument { get; set; }
        public string ZnakUpozorenja { get; set; }
        public string Status { get; set; }
        public string Html { get; set; }
    }
}
