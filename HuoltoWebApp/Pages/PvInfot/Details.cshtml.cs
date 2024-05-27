using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvInfot
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public PvInfo PvInfo { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PvInfos == null)
            {
                return NotFound();
            }

            var pvinfo = await _context.PvInfos
                .Include(pi => pi.Kuvat)
                .FirstOrDefaultAsync(m => m.PvInfoId == id);

            if (pvinfo == null)
            {
                return NotFound();
            }
            else 
            {
                PvInfo = pvinfo;
            }
            return Page();
        }
    }
}
