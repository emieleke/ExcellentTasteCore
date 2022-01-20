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
    public class BestellingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BestellingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bestellings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bestelling.Include(b => b.ConsumptieItem).Include(b => b.Reservering);
            
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> KokPage()
        {
            return View(await _context.Consumpties.Include(b => b.ConsumptieCode).Where(b => b.ConsumptieCode == "drk").ToListAsync());
        }
        public async Task<IActionResult> KokpageDetail(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumptie = await _context.Consumpties
                .Include(b => b.Consumpties)          
                .FirstOrDefaultAsync(m => m.ConsumptieCode == id);
                
            if (consumptie == null)
            {
                return NotFound();
            }

            return View(consumptie);
        }
        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.ConsumptieItem)
                .Include(b => b.Reservering)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestellings/Create
        public IActionResult Create()
        {
            ViewData["ConsumptieItemCode"] = new SelectList(_context.ConsumptieItems, "ConsumptieItemCode", "ConsumptieItemNaam");
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "ReserveringId", "Tafel");


            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BestellingId,ReserveringId,ConsumptieItemCode,Aantal,DateTimeBereidingConsumptie,Prijs,Totaal")] Bestelling bestelling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsumptieItemCode"] = new SelectList(_context.ConsumptieItems, "ConsumptieItemCode", "ConsumptieItemCode", bestelling.ConsumptieItemCode);
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "ReserveringId", "ReserveringId", bestelling.ReserveringId);
            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling.FindAsync(id);
            if (bestelling == null)
            {
                return NotFound();
            }
            ViewData["ConsumptieItemCode"] = new SelectList(_context.ConsumptieItems, "ConsumptieItemCode", "ConsumptieItemCode", bestelling.ConsumptieItemCode);
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "ReserveringId", "ReserveringId", bestelling.ReserveringId);
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingId,ReserveringId,ConsumptieItemCode,Aantal,DateTimeBereidingConsumptie,Prijs,Totaal")] Bestelling bestelling)
        {
            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.BestellingId))
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
            ViewData["ConsumptieItemCode"] = new SelectList(_context.ConsumptieItems, "ConsumptieItemCode", "ConsumptieItemCode", bestelling.ConsumptieItemCode);
            ViewData["ReserveringId"] = new SelectList(_context.Reservering, "ReserveringId", "ReserveringId", bestelling.ReserveringId);
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestelling
                .Include(b => b.ConsumptieItem)
                .Include(b => b.Reservering)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestelling.FindAsync(id);
            _context.Bestelling.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestelling.Any(e => e.BestellingId == id);
        }
    }
}
