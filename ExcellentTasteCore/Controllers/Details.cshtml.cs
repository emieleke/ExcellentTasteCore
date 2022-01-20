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
    public class DetailsModel : PageModel
    {
        private readonly ExcellentTasteCore.Data.ApplicationDbContext _context;

        public DetailsModel(ExcellentTasteCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
