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
    public class IndexModel : PageModel
    {
        private readonly ExcellentTasteCore.Data.ApplicationDbContext _context;

        public IndexModel(ExcellentTasteCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ConsumptieGroep> ConsumptieGroep { get;set; }

        public async Task OnGetAsync()
        {
            ConsumptieGroep = await _context.ConsumptieGroeps
                .Include(c => c.Consumptie).ToListAsync();
        }
    }
}
