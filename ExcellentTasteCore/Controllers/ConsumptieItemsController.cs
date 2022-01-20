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
    public class ConsumptieItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumptieItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsumptieItems
            
        public async Task<IActionResult> Index()
        {
            //DRANK TOEVOEGEN
            //Hier maak ik een string aan met een array met de volgende dranken afkorting
            IEnumerable<string> dranken = new string[] { "fik", "bik" };
            //hier geef ik aan de variabele dat wijst naar de context van consumptieitems en hij kijkt waar de dranken zit
            var applicationDbContext = _context.ConsumptieItems.Include(c => c.ConsumptieGroep).Where(c => dranken.Contains(c.ConsumptieGroepCode));
            //geeft het view terug
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConsumptieItems/Details/5
            public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumptieItem = await _context.ConsumptieItems
                .Include(c => c.ConsumptieGroep)
                .FirstOrDefaultAsync(m => m.ConsumptieItemCode == id);
            if (consumptieItem == null)
            {
                return NotFound();
            }

            return View(consumptieItem);
        }

        // GET: ConsumptieItems/Create
        public IActionResult Create()
        {
            ViewData["ConsumptieGroepCode"] = new SelectList(_context.ConsumptieGroeps, "ConsumptieGroepCode", "ConsumptieGroepCode");
            return View();
        }

        // POST: ConsumptieItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsumptieItemCode,ConsumptieGroepCode,ConsumptieItemNaam,Prijs")] ConsumptieItem consumptieItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumptieItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConsumptieGroepCode"] = new SelectList(_context.ConsumptieGroeps, "ConsumptieGroepCode", "ConsumptieGroepCode", consumptieItem.ConsumptieGroepCode);
            return View(consumptieItem);
        }

        // GET: ConsumptieItems/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumptieItem = await _context.ConsumptieItems.FindAsync(id);
            if (consumptieItem == null)
            {
                return NotFound();
            }
            ViewData["ConsumptieGroepCode"] = new SelectList(_context.ConsumptieGroeps, "ConsumptieGroepCode", "ConsumptieGroepCode", consumptieItem.ConsumptieGroepCode);
            return View(consumptieItem);
        }

        // POST: ConsumptieItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ConsumptieItemCode,ConsumptieGroepCode,ConsumptieItemNaam,Prijs")] ConsumptieItem consumptieItem)
        {
            if (id != consumptieItem.ConsumptieItemCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumptieItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumptieItemExists(consumptieItem.ConsumptieItemCode))
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
            ViewData["ConsumptieGroepCode"] = new SelectList(_context.ConsumptieGroeps, "ConsumptieGroepCode", "ConsumptieGroepCode", consumptieItem.ConsumptieGroepCode);
            return View(consumptieItem);
        }

        // GET: ConsumptieItems/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumptieItem = await _context.ConsumptieItems
                .Include(c => c.ConsumptieGroep)
                .FirstOrDefaultAsync(m => m.ConsumptieItemCode == id);
            if (consumptieItem == null)
            {
                return NotFound();
            }

            return View(consumptieItem);
        }

        // POST: ConsumptieItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var consumptieItem = await _context.ConsumptieItems.FindAsync(id);
            _context.ConsumptieItems.Remove(consumptieItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumptieItemExists(string id)
        {
            return _context.ConsumptieItems.Any(e => e.ConsumptieItemCode == id);
        }
    }
}
