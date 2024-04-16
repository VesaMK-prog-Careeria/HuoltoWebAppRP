using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönHuoltopyyntö
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public SäiliöHuoltopyyntö SäiliöHuoltopyyntö { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöHuoltopyyntös == null)
            {
                return NotFound();
            }

            var säiliöhuoltopyyntö = await _context.SäiliöHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (säiliöhuoltopyyntö == null)
            {
                return NotFound();
            }
            else 
            {
                SäiliöHuoltopyyntö = säiliöhuoltopyyntö;
            }
            return Page();
        }
    }
}
