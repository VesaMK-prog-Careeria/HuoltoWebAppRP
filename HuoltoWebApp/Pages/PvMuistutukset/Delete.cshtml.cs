using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvMuistutukset
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
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

            var pvmuistutu = await _context.PvMuistutus.FirstOrDefaultAsync(m => m.PvMuistutusId == id);

            if (pvmuistutu == null)
            {
                return NotFound();
            }
            else 
            {
                PvMuistutu = pvmuistutu;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PvMuistutus == null)
            {
                return NotFound();
            }
            var pvmuistutu = await _context.PvMuistutus.FindAsync(id);

            if (pvmuistutu != null)
            {
                PvMuistutu = pvmuistutu;
                _context.PvMuistutus.Remove(PvMuistutu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
