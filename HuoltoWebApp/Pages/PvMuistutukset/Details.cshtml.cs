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
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

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
    }
}
