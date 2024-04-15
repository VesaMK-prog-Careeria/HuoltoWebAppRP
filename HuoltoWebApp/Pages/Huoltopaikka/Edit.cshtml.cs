using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Huoltopaikka
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
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

            var huoltopaikat =  await _context.Huoltopaikats.FirstOrDefaultAsync(m => m.HuoltoPaikkaId == id);
            if (huoltopaikat == null)
            {
                return NotFound();
            }
            Huoltopaikat = huoltopaikat;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Huoltopaikat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HuoltopaikatExists(Huoltopaikat.HuoltoPaikkaId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool HuoltopaikatExists(int id)
        {
          return (_context.Huoltopaikats?.Any(e => e.HuoltoPaikkaId == id)).GetValueOrDefault();
        }
    }
}
