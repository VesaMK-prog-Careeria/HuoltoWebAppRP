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

namespace HuoltoWebApp.Pages.SäiliönMuistutus
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SäiliöMuistutu SäiliöMuistutu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöMuistutus == null)
            {
                return NotFound();
            }

            var säiliömuistutu =  await _context.SäiliöMuistutus.FirstOrDefaultAsync(m => m.SäiliöMuistutusId == id);
            if (säiliömuistutu == null)
            {
                return NotFound();
            }
            SäiliöMuistutu = säiliömuistutu;
           ViewData["Muistutustyyppi"] = new SelectList(_context.Muistutustyyppis, "MuistutustyyppiId", "MuistutustyyppiId");
           ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
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

            _context.Attach(SäiliöMuistutu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SäiliöMuistutuExists(SäiliöMuistutu.SäiliöMuistutusId))
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

        private bool SäiliöMuistutuExists(int id)
        {
          return (_context.SäiliöMuistutus?.Any(e => e.SäiliöMuistutusId == id)).GetValueOrDefault();
        }
    }
}
