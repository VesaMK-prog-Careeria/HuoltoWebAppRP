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

namespace HuoltoWebApp.Pages.AutonMuistutus
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AutoMuistutu AutoMuistutu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoMuistutus == null)
            {
                return NotFound();
            }

            var automuistutu =  await _context.AutoMuistutus.FirstOrDefaultAsync(m => m.AutoMuistutusId == id);
            if (automuistutu == null)
            {
                return NotFound();
            }
            AutoMuistutu = automuistutu;
           ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoId");
           ViewData["Muistutustyyppi"] = new SelectList(_context.Muistutustyyppis, "MuistutustyyppiId", "MuistutustyyppiId");
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

            _context.Attach(AutoMuistutu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoMuistutuExists(AutoMuistutu.AutoMuistutusId))
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

        private bool AutoMuistutuExists(int id)
        {
          return (_context.AutoMuistutus?.Any(e => e.AutoMuistutusId == id)).GetValueOrDefault();
        }
    }
}
