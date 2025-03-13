using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Autot
{
    public class DeleteModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DeleteModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Auto Auto { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var auto = await _context.Autos.FirstOrDefaultAsync(m => m.AutoId == id);

            if (auto == null)
            {
                return NotFound();
            }
            else 
            {
                Auto = auto;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }
            var auto = await _context.Autos
                .Include(a => a.AutoInfo) // Varmista, että lataat myös riippuvaiset AutoInfo-entiteetit
                .Include(a => a.AutoHuoltopyyntös) // Varmista, että lataat myös riippuvaiset AutoHuoltopyynnöt-entiteetit
                .FirstOrDefaultAsync(m => m.AutoId == id);

            if (auto == null)
            {
                return NotFound();
            }

            // Poista ensin kaikki riippuvaiset AutoInfo-tietueet
            if (auto.AutoInfo != null)
            {
                _context.AutoInfos.RemoveRange(auto.AutoInfo);
                //await _context.SaveChangesAsync(); // Tallenna muutokset ennen auton poistoa
            }

            // Poista kaikki autoon liittyvät huoltopyynnöt
            if (auto.AutoHuoltopyyntös != null)
            {
                _context.AutoHuoltopyyntös.RemoveRange(auto.AutoHuoltopyyntös);
            }

            await _context.SaveChangesAsync();

            // Poista itse auto
            _context.Autos.Remove(auto);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
