using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Pvt
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pv Pv { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pvs == null)
            {
                return NotFound();
            }

            var pv = await _context.Pvs.FirstOrDefaultAsync(m => m.PvId == id);

            if (pv == null)
            {
                return NotFound();
            }
            else
            {
               Pv = pv;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pvs == null)
            {
                return NotFound();
            }
            var pv = await _context.Pvs
                .Include(a => a.PvInfo)// Varmista, että lataat myös riippuvaiset PVInfo-entiteetit
                .FirstOrDefaultAsync(m => m.PvId == id);

            if (pv == null)
            {
                return NotFound();
            }

            // Poista ensin kaikki riippuvaiset PvInfo-tietueet
            if (pv.PvInfo != null)
            {
                _context.PvInfos.RemoveRange(pv.PvInfo);
                await _context.SaveChangesAsync(); // Tallenna muutokset ennen pvn poistoa
            }

            // Poista itse pv
            _context.Pvs.Remove(pv);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
