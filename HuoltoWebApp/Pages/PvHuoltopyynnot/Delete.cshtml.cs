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
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PvHuoltopyyntös == null)
            {
                return NotFound();
            }
            var pvhuoltopyyntö = await _context.PvHuoltopyyntös.FindAsync(id);

            if (pvhuoltopyyntö != null)
            {
                PvHuoltopyyntö = pvhuoltopyyntö;
                _context.PvHuoltopyyntös.Remove(PvHuoltopyyntö);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
