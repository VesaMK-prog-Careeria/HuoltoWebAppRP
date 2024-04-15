using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Muistutustyypit
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public Muistutustyyppi Muistutustyyppi { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Muistutustyyppis == null)
            {
                return NotFound();
            }

            var muistutustyyppi = await _context.Muistutustyyppis.FirstOrDefaultAsync(m => m.MuistutustyyppiId == id);
            if (muistutustyyppi == null)
            {
                return NotFound();
            }
            else 
            {
                Muistutustyyppi = muistutustyyppi;
            }
            return Page();
        }
    }
}
