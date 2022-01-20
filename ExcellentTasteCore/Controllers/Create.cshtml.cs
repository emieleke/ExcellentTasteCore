using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExcellentTasteCore.Data;
using ExcellentTasteCore.Models;

namespace ExcellentTasteCore.Controllers
{
    public class CreateModel : PageModel
    {
        private readonly ExcellentTasteCore.Data.ApplicationDbContext _context;

        public CreateModel(ExcellentTasteCore.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ConsumptieCode"] = new SelectList(_context.Consumpties, "ConsumptieCode", "ConsumptieCode");
            return Page();
        }

        [BindProperty]
        public ConsumptieGroep ConsumptieGroep { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ConsumptieGroeps.Add(ConsumptieGroep);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
