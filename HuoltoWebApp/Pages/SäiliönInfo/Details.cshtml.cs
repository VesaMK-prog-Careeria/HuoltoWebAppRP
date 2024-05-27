using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönInfo
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public SäiliöInfo SäiliöInfo { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöInfos == null)
            {
                return NotFound();
            }

            var säiliöinfo = await _context.SäiliöInfos
                .Include(si => si.Kuvat)
                .FirstOrDefaultAsync(m => m.SäiliöInfoId == id);

            if (säiliöinfo == null)
            {
                return NotFound();
            }
            else 
            {
                SäiliöInfo = säiliöinfo;
            }
            return Page();
        }
    }
}
