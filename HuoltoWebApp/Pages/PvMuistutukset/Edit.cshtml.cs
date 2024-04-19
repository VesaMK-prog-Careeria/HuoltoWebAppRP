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

namespace HuoltoWebApp.Pages.PvMuistutukset
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PvMuistutu PvMuistutu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PvMuistutus == null)
            {
                return NotFound();
            }

            var pvmuistutu =  await _context.PvMuistutus.FirstOrDefaultAsync(m => m.PvMuistutusId == id);
            if (pvmuistutu == null)
            {
                return NotFound();
            }
            PvMuistutu = pvmuistutu;
           ViewData["Muistutustyyppi"] = new SelectList(_context.Muistutustyyppis, "MuistutustyyppiId", "MuistutustyyppiId");
           ViewData["PvId"] = new SelectList(_context.Pvs, "PvId", "PvId");
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

            _context.Attach(PvMuistutu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PvMuistutuExists(PvMuistutu.PvMuistutusId))
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

        private bool PvMuistutuExists(int id)
        {
          return (_context.PvMuistutus?.Any(e => e.PvMuistutusId == id)).GetValueOrDefault();
        }
    }
}
