using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PremiumDental.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using System.Web;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace PremiumDental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PremiumDbContext Context { get; }
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, PremiumDbContext _context, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.Context = _context;
            this._hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(PremiumDbContext context)
        {
           
            var lista = (from Doktori in this.Context.Doktoris select Doktori).ToList();

            ViewBag.DokLista = new SelectList(lista,"ID","Ime","Prezime");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Karton()
        {
            Pacijenti pac = new Pacijenti();
          
            var lista = (from Pacijenti in this.Context.Pacijentis select Pacijenti).ToList();
           

            ViewBag.lista = lista;
            ViewBag.pac = pac;

            return View();
            //Pacijenti list = new Pacijenti();
            //return View(list);
           
        }

        public IActionResult PrikaziKarton(Pacijenti pacijenti)
        {
            int mojid = Int32.Parse(pacijenti.Id.ToString());
            pacijenti = (from a in Context.Pacijentis
                       where a.Id == mojid
                       select a).SingleOrDefault();
            var lista = Context.Pacijentis.ToList();
            ViewBag.lista = lista;
            if(pacijenti.Snimak != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var folder =  pacijenti.Ime + " " + pacijenti.Prezime + " " + pacijenti.DatumRodjenja ;
                List<IFileInfo> files = _hostEnvironment.WebRootFileProvider.GetDirectoryContents(folder).ToList();
                ViewBag.snimci = files;
                ViewBag.folder = folder;
            }
          

            TempData["Karton"] = pacijenti.Html;
           
            return View("Karton", pacijenti);
            //Pacijenti list = new Pacijenti();
            //return View(list);

        }
        //public IActionResult Tabela()
        //{

        //    List<Pacijenti> pacijentis = (from Pacijenti in this.Context.Pacijentis.Take(10)
        //                                  select Pacijenti).ToList();
        //    return View(pacijentis);




        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult GetTermin4Terminal( DateTime start, DateTime end, int tip)
        {


            try
            {
                using (PremiumDbContext db = new PremiumDbContext())
                {

                    var res = (from x in db.Zakazivanjes
                               where ((x.VremeOd >= start && x.VremeOd <= end) || (x.VremeOd >= start))
                               select x).ToList()
                       .Select(o => new
                       {

                           Idd = o.Id,
                           title = o.Usluga + " " + o.Ime + " " + o.Prezime,                         
                           ime = o.Ime,
                           prezime = o.Prezime,
                           VremeOD = o.VremeOd,
                           VremeDO = o.VremeDo,
                           doktor = o.Doktor,
                           usluga = o.Usluga,
                           napomena = o.Napomena,
                           docek = o.Docek,
                           smestaj = o.Smestaj,
                           start = o.VremeOd,
                           end = o.VremeDo,
                           color = o.Boja,
                           note = o.Napomena,
                           tel = o.Telefon,
                           mail = o.Email,
                           textColor = "black",
                           resourceId = o.Doktor



                       }
                       ); 
                    return Json(res.ToArray());
                }
            }

            catch (Exception e)
            {

                return Json("Servis trenutno nije dostupan");
            }

        }

        public string Zakazi(string Ime, string Prezime, DateTime VremeOd, int VremeDo, int Doktor, string Usluga, string Napomena, string Docek, string Smestaj, string Email, string Telefon)
        {
            using (PremiumDbContext db = new PremiumDbContext())
            {
                Termini ter = new Termini();
                ter.Ime = Ime;
            ter.Prezime = Prezime;
            ter.VremeOd = VremeOd;
            ter.VremeDo = VremeOd.AddMinutes(VremeDo);
            ter.Doktor = Doktor;
            ter.Usluga = Usluga;
            ter.Napomena = Napomena;
            ter.Docek = Docek;
            ter.Smestaj = Smestaj;
            ter.Email = Email;
            ter.Telefon = Telefon;
            db.Add(ter);
            db.SaveChanges();
        }
            return "Termin uspešno zakazan.";
        }
        public string Otkazi(int idd)
        {
            using (PremiumDbContext db = new PremiumDbContext())
            {
                var x = (from y in db.Terminis
                         where y.Id == idd
                         select y).FirstOrDefault();
                db.Terminis.Remove(x);
                db.SaveChanges();
                return "Termin uspešno otkazan";
            }
        }
        public string Azuriraj(int idd,string Ime,string datum, string Prezime, TimeSpan VremeOd, TimeSpan VremeDo, int Doktor, string Usluga, string Napomena, string Docek, string Smestaj, string Email, string Telefon)
        {
            DateTime dat = DateTime.Parse(datum);
            DateTime od = dat.Add(VremeOd);
           DateTime doo = dat.Add(VremeDo);
          
            
            using (PremiumDbContext db = new PremiumDbContext())
            {
                Termini ter = (from x in db.Terminis
                               where x.Id == idd
                               select x).FirstOrDefault();
                ter.Ime = Ime;
                ter.Prezime = Prezime;
                ter.VremeOd = od;
                ter.VremeDo = doo;
                ter.Doktor = Doktor;
                ter.Usluga = Usluga;
                ter.Napomena = Napomena;
                ter.Docek = Docek;
                ter.Smestaj = Smestaj;
                ter.Email = Email;
                ter.Telefon = Telefon;
                db.Update(ter);
                db.SaveChanges();
            }
            return "Uspesno ste azurirali podatke";
        }
    }
}
