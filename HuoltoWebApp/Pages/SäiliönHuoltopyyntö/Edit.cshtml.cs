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

namespace HuoltoWebApp.Pages.SäiliönHuoltopyyntö
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SäiliöHuoltopyyntö SäiliöHuoltopyyntö { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöHuoltopyyntös == null)
            {
                return NotFound();
            }

            var säiliöhuoltopyyntö =  await _context.SäiliöHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (säiliöhuoltopyyntö == null)
            {
                return NotFound();
            }
            SäiliöHuoltopyyntö = säiliöhuoltopyyntö;
           ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
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

            _context.Attach(SäiliöHuoltopyyntö).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SäiliöHuoltopyyntöExists(SäiliöHuoltopyyntö.HuoltoId))
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

        private bool SäiliöHuoltopyyntöExists(int id)
        {
          return (_context.SäiliöHuoltopyyntös?.Any(e => e.HuoltoId == id)).GetValueOrDefault();
        }
    }
}
