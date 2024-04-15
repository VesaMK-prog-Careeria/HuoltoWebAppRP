using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Huoltopaikka
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Huoltopaikat Huoltopaikat { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Huoltopaikats == null)
            {
                return NotFound();
            }

            var huoltopaikat = await _context.Huoltopaikats.FirstOrDefaultAsync(m => m.HuoltoPaikkaId == id);

            if (huoltopaikat == null)
            {
                return NotFound();
            }
            else 
            {
                Huoltopaikat = huoltopaikat;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Huoltopaikats == null)
            {
                return NotFound();
            }
            var huoltopaikat = await _context.Huoltopaikats.FindAsync(id);

            if (huoltopaikat != null)
            {
                Huoltopaikat = huoltopaikat;
                _context.Huoltopaikats.Remove(Huoltopaikat);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
