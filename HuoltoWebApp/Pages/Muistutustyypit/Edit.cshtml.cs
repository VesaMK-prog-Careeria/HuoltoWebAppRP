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

namespace HuoltoWebApp.Pages.Muistutustyypit
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Muistutustyyppi Muistutustyyppi { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Muistutustyyppis == null)
            {
                return NotFound();
            }

            var muistutustyyppi =  await _context.Muistutustyyppis.FirstOrDefaultAsync(m => m.MuistutustyyppiId == id);
            if (muistutustyyppi == null)
            {
                return NotFound();
            }
            Muistutustyyppi = muistutustyyppi;
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

            _context.Attach(Muistutustyyppi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MuistutustyyppiExists(Muistutustyyppi.MuistutustyyppiId))
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

        private bool MuistutustyyppiExists(int id)
        {
          return (_context.Muistutustyyppis?.Any(e => e.MuistutustyyppiId == id)).GetValueOrDefault();
        }
    }
}
