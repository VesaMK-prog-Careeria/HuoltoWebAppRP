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

namespace HuoltoWebApp.Pages.PvHuoltopyynnot
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
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

            var pvhuoltopyyntö =  await _context.PvHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (pvhuoltopyyntö == null)
            {
                return NotFound();
            }
            PvHuoltopyyntö = pvhuoltopyyntö;
           ViewData["PvId"] = new SelectList(_context.Pvs, "PvId", "PvId");
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

            _context.Attach(PvHuoltopyyntö).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PvHuoltopyyntöExists(PvHuoltopyyntö.HuoltoId))
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

        private bool PvHuoltopyyntöExists(int id)
        {
          return (_context.PvHuoltopyyntös?.Any(e => e.HuoltoId == id)).GetValueOrDefault();
        }
    }
}
