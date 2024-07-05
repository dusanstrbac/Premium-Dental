using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PremiumDental.Models;

namespace PremiumDental.Controllers
{
    public class KartonsController : Controller
    {
        private readonly PremiumDbContext _context;

        public KartonsController(PremiumDbContext context)
        {
            _context = context;
        }

        // GET: Kartons
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kartons.ToListAsync());
        }

        // GET: Kartons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Kartons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karton == null)
            {
                return NotFound();
            }

            return View(karton);
        }

        // GET: Kartons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kartons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BrojKartona,ZnakUpozorenja,Status,Html")] Karton karton)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karton);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karton);
        }

        // GET: Kartons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Kartons.FindAsync(id);
            if (karton == null)
            {
                return NotFound();
            }
            return View(karton);
        }

        // POST: Kartons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BrojKartona,ZnakUpozorenja,Status,Html")] Karton karton)
        {
            if (id != karton.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karton);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KartonExists(karton.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(karton);
        }

        // GET: Kartons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karton = await _context.Kartons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (karton == null)
            {
                return NotFound();
            }

            return View(karton);
        }

        // POST: Kartons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karton = await _context.Kartons.FindAsync(id);
            _context.Kartons.Remove(karton);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KartonExists(int id)
        {
            return _context.Kartons.Any(e => e.Id == id);
        }
    }
}
