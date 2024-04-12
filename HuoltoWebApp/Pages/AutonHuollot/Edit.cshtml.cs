using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AutoHuollot AutoHuollot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoHuollots == null)
            {
                return NotFound();
            }

            var autohuollot =  await _context.AutoHuollots.FirstOrDefaultAsync(m => m.HuollonId == id);
            if (autohuollot == null)
            {
                return NotFound();
            }
            AutoHuollot = autohuollot;
           ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoId");
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

            _context.Attach(AutoHuollot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoHuollotExists(AutoHuollot.HuollonId))
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

        private bool AutoHuollotExists(int id)
        {
          return (_context.AutoHuollots?.Any(e => e.HuollonId == id)).GetValueOrDefault();
        }
    }
}
