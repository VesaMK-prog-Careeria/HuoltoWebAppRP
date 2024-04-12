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

namespace HuoltoWebApp.Pages.AutonHuoltopyyntö
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AutoHuoltopyyntö AutoHuoltopyyntö { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoHuoltopyyntös == null)
            {
                return NotFound();
            }

            var autohuoltopyyntö =  await _context.AutoHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (autohuoltopyyntö == null)
            {
                return NotFound();
            }
            AutoHuoltopyyntö = autohuoltopyyntö;
           ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoId");
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

            _context.Attach(AutoHuoltopyyntö).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoHuoltopyyntöExists(AutoHuoltopyyntö.HuoltoId))
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

        private bool AutoHuoltopyyntöExists(int id)
        {
          return (_context.AutoHuoltopyyntös?.Any(e => e.HuoltoId == id)).GetValueOrDefault();
        }
    }
}
