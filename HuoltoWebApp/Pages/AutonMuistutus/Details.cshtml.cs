using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonMuistutus
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public AutoMuistutu AutoMuistutu { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoMuistutus == null)
            {
                return NotFound();
            }

            var automuistutu = await _context.AutoMuistutus.FirstOrDefaultAsync(m => m.AutoMuistutusId == id);
            if (automuistutu == null)
            {
                return NotFound();
            }
            else 
            {
                AutoMuistutu = automuistutu;
            }
            return Page();
        }
    }
}
