using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönMuistutus
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
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

            var säiliömuistutu = await _context.SäiliöMuistutus.FirstOrDefaultAsync(m => m.SäiliöMuistutusId == id);

            if (säiliömuistutu == null)
            {
                return NotFound();
            }
            else 
            {
                SäiliöMuistutu = säiliömuistutu;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SäiliöMuistutus == null)
            {
                return NotFound();
            }
            var säiliömuistutu = await _context.SäiliöMuistutus.FindAsync(id);

            if (säiliömuistutu != null)
            {
                SäiliöMuistutu = säiliömuistutu;
                _context.SäiliöMuistutus.Remove(SäiliöMuistutu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
