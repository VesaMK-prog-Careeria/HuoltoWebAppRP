using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvHuolto
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PvHuollot PvHuollot { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PvHuollots == null)
            {
                return NotFound();
            }

            var pvhuollot = await _context.PvHuollots.FirstOrDefaultAsync(m => m.HuoltoId == id);

            if (pvhuollot == null)
            {
                return NotFound();
            }
            else 
            {
                PvHuollot = pvhuollot;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PvHuollots == null)
            {
                return NotFound();
            }
            var pvhuollot = await _context.PvHuollots.FindAsync(id);

            if (pvhuollot != null)
            {
                PvHuollot = pvhuollot;
                _context.PvHuollots.Remove(PvHuollot);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
