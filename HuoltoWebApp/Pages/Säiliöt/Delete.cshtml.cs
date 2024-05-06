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
            var säiliö = await _context.Säiliös
                .Include(s => s.SäiliöInfo) // Varmista, että lataat myös riippuvaiset AutoInfo-entiteetit
                .FirstOrDefaultAsync(m => m.SäiliöId == id);

            if (säiliö == null)
            {
                return NotFound();
            }

            // Poista ensin kaikki riippuvaiset AutoInfo-tietueet
            if (säiliö.SäiliöInfo != null)
            {
                _context.SäiliöInfos.RemoveRange(säiliö.SäiliöInfo);
                await _context.SaveChangesAsync(); // Tallenna muutokset ennen auton poistoa
            }

            // Poista itse auto
            _context.Säiliös.Remove(säiliö);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
