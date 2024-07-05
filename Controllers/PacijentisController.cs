using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PremiumDental.Models;

namespace PremiumDental.Controllers
{
    public class PacijentisController : Controller
    {
        private readonly PremiumDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PacijentisController(PremiumDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Pacijentis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacijentis.ToListAsync());
        }

        // GET: Pacijentis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijenti == null)
            {
                return NotFound();
            }

            return View(pacijenti);
        }

        // GET: Pacijentis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacijentis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Email,Telefon,Telefon2,Pol,Jmbg,Adresa,Grad,BrojKartona,DatumRodjenja,LicniDokument,ZnakUpozorenja,Status,Html,Snimak,file")] Pacijenti pacijenti)
        {
            if (ModelState.IsValid)
            {
                if(pacijenti.Id != 0)
                {
                    if (pacijenti.file != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(pacijenti.file.FileName);
                        string extension = Path.GetExtension(pacijenti.file.FileName);
                        pacijenti.Snimak = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        var folder = wwwRootPath + "/" +  pacijenti.Ime + " " + pacijenti.Prezime + " " + pacijenti.DatumRodjenja ;
                        string path = Path.Combine( folder, fileName);
                        bool postoji = Directory.Exists(folder);
                        if (!postoji)
                        {
                            Directory.CreateDirectory(folder);
                        }
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await pacijenti.file.CopyToAsync(fileStream);
                        }
                    }
                    _context.Update(pacijenti);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Karton", "Home");
                }
                else
                {
                    if(pacijenti.file != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(pacijenti.file.FileName);
                        string extension = Path.GetExtension(pacijenti.file.FileName);
                        pacijenti.Snimak = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/'" + pacijenti.Ime + "" + pacijenti.Prezime + " " + pacijenti.DatumRodjenja + "'/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await pacijenti.file.CopyToAsync(fileStream);
                        }
                    }
                   

                    var sad = pacijenti.Ime;
                    var ds = pacijenti.Html;
                    _context.Add(pacijenti);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Karton", "Home");
                }
               
               
            }
            return RedirectToAction("Karton", "Home");
        }

        // GET: Pacijentis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis.FindAsync(id);
            if (pacijenti == null)
            {
                return NotFound();
            }
            return View(pacijenti);
        }

        // POST: Pacijentis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Email,Telefon,Telefon2,Pol,Jmbg,Adresa,Grad,BrojKartona,DatumRodjenja,LicniDokument,ZnakUpozorenja,Status,Html")] Pacijenti pacijenti)
        {
            if (id != pacijenti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacijenti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacijentiExists(pacijenti.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("PrikaziKarton", "Home");
            }
            return RedirectToAction("PrikaziKarton", "Home");
        }

        // GET: Pacijentis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacijenti = await _context.Pacijentis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacijenti == null)
            {
                return NotFound();
            }

            return View(pacijenti);
        }

        // POST: Pacijentis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacijenti = await _context.Pacijentis.FindAsync(id);
            _context.Pacijentis.Remove(pacijenti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacijentiExists(int id)
        {
            return _context.Pacijentis.Any(e => e.Id == id);
        }
    }
}
