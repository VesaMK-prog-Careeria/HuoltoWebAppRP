using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvHuoltopyynnot
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public PvHuoltopyyntö PvHuoltopyyntö { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PvHuoltopyyntös == null)
            {
                return NotFound();
            }

            var pvhuoltopyyntö = await _context.PvHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (pvhuoltopyyntö == null)
            {
                return NotFound();
            }
            else 
            {
                PvHuoltopyyntö = pvhuoltopyyntö;
            }
            return Page();
        }
    }
}
