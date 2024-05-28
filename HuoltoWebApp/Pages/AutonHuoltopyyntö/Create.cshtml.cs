using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.AutonHuoltopyyntö
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AutoHuoltopyyntö AutoHuoltopyyntö { get; set; } = new AutoHuoltopyyntö();

        public async Task<IActionResult> OnGetAsync(int? autoId)
        {
            if (autoId == null)
            {
                return NotFound();
            }

            // Haetaan Auto annettujen autoId perusteella
            var auto = await _context.Autos.FindAsync(autoId);

            if (auto == null)
            {
                return NotFound();
            }

            // Asetetaan rekisterinumero ViewDataan
            ViewData["AutoRekNro"] = auto.RekNro;
            AutoHuoltopyyntö.AutoId = autoId.Value;

            return Page();

        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var auto = await _context.Autos.FindAsync(AutoHuoltopyyntö.AutoId);

            if (auto == null)
            {
                ModelState.AddModelError("AutoHuoltopyyntö.HuollonKuvaus", "Ei huoltoa");
                return Page();
            }

            _context.AutoHuoltopyyntös.Add(AutoHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
