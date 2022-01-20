using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcellentTasteCore.Data;
using ExcellentTasteCore.Models;

namespace ExcellentTasteCore.Controllers
{
    public class ReserveringsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReserveringsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reserverings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservering.Include(r => r.Klant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reserverings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .Include(r => r.Klant)
                .FirstOrDefaultAsync(m => m.ReserveringId == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // GET: Reserverings/Create
        public IActionResult Create()
        {
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "KlantNaam");
            return View();
        }

        // POST: Reserverings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReserveringId,KlantId,Datum,Tijd,Tafel,Straat,Huisnummer,Toevoeging,Postcode,Woonplaats,Land,AantalPersonen,Status,DatumToegevoegd,BonDatum,Betalingswijze,BonTotaal")] Reservering reservering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservering);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "KlantNaam", reservering.KlantId);
            return View(reservering);
        }

        // GET: Reserverings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering.FindAsync(id);
            if (reservering == null)
            {
                return NotFound();
            }
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "KlantNaam", reservering.KlantId);
            return View(reservering);
        }

        // POST: Reserverings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReserveringId,KlantId,Datum,Tijd,Tafel,Straat,Huisnummer,Toevoeging,Postcode,Woonplaats,Land,AantalPersonen,Status,DatumToegevoegd,BonDatum,Betalingswijze,BonTotaal")] Reservering reservering)
        {
            if (id != reservering.ReserveringId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReserveringExists(reservering.ReserveringId))
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
            ViewData["KlantId"] = new SelectList(_context.Klant, "KlantId", "KlantNaam", reservering.KlantId);
            return View(reservering);
        }

        // GET: Reserverings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservering = await _context.Reservering
                .Include(r => r.Klant)
                .FirstOrDefaultAsync(m => m.ReserveringId == id);
            if (reservering == null)
            {
                return NotFound();
            }

            return View(reservering);
        }

        // POST: Reserverings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservering = await _context.Reservering.FindAsync(id);
            _context.Reservering.Remove(reservering);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReserveringExists(int id)
        {
            return _context.Reservering.Any(e => e.ReserveringId == id);
        }
    }
}
