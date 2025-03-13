using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Säiliöt
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public Säiliö Säiliö { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Säiliös == null)
            {
                return NotFound();
            }

            var säiliö = await _context.Säiliös
                .Include(s => s.SäiliöInfo) // Tässä haetaan SäiliöInfo-taulusta tiedot
                .FirstOrDefaultAsync(m => m.SäiliöId == id);
            if (säiliö == null)
            {
                return NotFound();
            }
            else 
            {
                Säiliö = säiliö;
            }
            return Page();
        }
    }
}
