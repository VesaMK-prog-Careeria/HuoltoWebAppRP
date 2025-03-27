using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Pvt
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public Pv Pv { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pvs == null)
            {
                return NotFound();
            }

            var pv = await _context.Pvs
                .Include(s => s.PvInfo) // Tässä haetaan PvInfo-taulusta tiedot
                .FirstOrDefaultAsync(m => m.PvId == id);
            if (pv == null)
            {
                return NotFound();
            }
            else 
            {
                Pv = pv;
            }
            return Page();
        }
    }
}
