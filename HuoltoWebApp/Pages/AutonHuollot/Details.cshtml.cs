using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public AutoHuollot AutoHuollot { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoHuollots == null)
            {
                return NotFound();
            }

            var autohuollot = await _context.AutoHuollots.FirstOrDefaultAsync(m => m.HuollonId == id);
            if (autohuollot == null)
            {
                return NotFound();
            }
            else 
            {
                AutoHuollot = autohuollot;
            }
            return Page();
        }
    }
}
