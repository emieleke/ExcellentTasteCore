using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExcellentTasteCore.Data;
using ExcellentTasteCore.Models;

namespace ExcellentTasteCore.Controllers
{
    public class DeleteModel : PageModel
    {
        private readonly ExcellentTasteCore.Data.ApplicationDbContext _context;

        public DeleteModel(ExcellentTasteCore.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ConsumptieGroep = await _context.ConsumptieGroeps.FindAsync(id);

            if (ConsumptieGroep != null)
            {
                _context.ConsumptieGroeps.Remove(ConsumptieGroep);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
