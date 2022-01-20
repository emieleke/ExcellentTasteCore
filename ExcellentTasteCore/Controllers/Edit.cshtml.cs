using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExcellentTasteCore.Data;
using ExcellentTasteCore.Models;

namespace ExcellentTasteCore.Controllers
{
    public class EditModel : PageModel
    {
        private readonly ExcellentTasteCore.Data.ApplicationDbContext _context;

        public EditModel(ExcellentTasteCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ConsumptieGroep ConsumptieGroep { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptieGroep = await _context.ConsumptieGroeps
                .Include(c => c.Consumptie).FirstOrDefaultAsync(m => m.ConsumptieGroepCode == id);

            if (ConsumptieGroep == null)
            {
                return NotFound();
            }
           ViewData["ConsumptieCode"] = new SelectList(_context.Consumpties, "ConsumptieCode", "ConsumptieCode");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ConsumptieGroep).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumptieGroepExists(ConsumptieGroep.ConsumptieGroepCode))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ConsumptieGroepExists(string id)
        {
            return _context.ConsumptieGroeps.Any(e => e.ConsumptieGroepCode == id);
        }
    }
}
