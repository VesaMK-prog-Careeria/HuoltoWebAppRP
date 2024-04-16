using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönHuollot
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public SäiliöHuollot SäiliöHuollot { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöHuollots == null)
            {
                return NotFound();
            }

            var säiliöhuollot = await _context.SäiliöHuollots.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (säiliöhuollot == null)
            {
                return NotFound();
            }
            else 
            {
                SäiliöHuollot = säiliöhuollot;
            }
            return Page();
        }
    }
}
