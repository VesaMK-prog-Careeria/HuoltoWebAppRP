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
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public SäiliöInfo SäiliöInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöInfos == null)
            {
                return NotFound();
            }

            var säiliöinfo = await _context.SäiliöInfos.FirstOrDefaultAsync(m => m.SäiliöInfoId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SäiliöInfos == null)
            {
                return NotFound();
            }
            var säiliöinfo = await _context.SäiliöInfos.FindAsync(id);

            if (säiliöinfo != null)
            {
                SäiliöInfo = säiliöinfo;
                _context.SäiliöInfos.Remove(SäiliöInfo);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
