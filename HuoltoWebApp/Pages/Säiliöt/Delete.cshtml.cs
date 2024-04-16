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
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Säiliö Säiliö { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Säiliös == null)
            {
                return NotFound();
            }

            var säiliö = await _context.Säiliös.FirstOrDefaultAsync(m => m.SäiliöId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Säiliös == null)
            {
                return NotFound();
            }
            var säiliö = await _context.Säiliös.FindAsync(id);

            if (säiliö != null)
            {
                Säiliö = säiliö;
                _context.Säiliös.Remove(Säiliö);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
