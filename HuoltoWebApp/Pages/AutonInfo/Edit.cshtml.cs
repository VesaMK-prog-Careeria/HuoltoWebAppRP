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

namespace HuoltoWebApp.Pages.AutonInfo
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AutoInfo AutoInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoInfos == null)
            {
                return NotFound();
            }

            var autoinfo =  await _context.AutoInfos.FirstOrDefaultAsync(m => m.AutoInfoId == id);
            if (autoinfo == null)
            {
                return NotFound();
            }
            AutoInfo = autoinfo;
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

            _context.Attach(AutoInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoInfoExists(AutoInfo.AutoInfoId))
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

        private bool AutoInfoExists(int id)
        {
          return (_context.AutoInfos?.Any(e => e.AutoInfoId == id)).GetValueOrDefault();
        }
    }
}
