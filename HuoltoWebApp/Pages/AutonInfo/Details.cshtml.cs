using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonInfo
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public AutoInfo AutoInfo { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoInfos == null)
            {
                return NotFound();
            }

            var autoinfo = await _context.AutoInfos.FirstOrDefaultAsync(m => m.AutoInfoId == id);
            if (autoinfo == null)
            {
                return NotFound();
            }
            else 
            {
                AutoInfo = autoinfo;
            }
            return Page();
        }
    }
}
