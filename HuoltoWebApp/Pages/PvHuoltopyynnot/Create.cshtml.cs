using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvHuoltopyynnot
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PvHuoltopyyntö PvHuoltopyyntö { get; set; } = new PvHuoltopyyntö();

        public async Task<IActionResult> OnGetAsync(int? pvId)
        {
            if (pvId == null)
            {
                return NotFound();
            }

            // Haetaan Auto annettujen autoId perusteella
            var pv = await _context.Pvs.FindAsync(pvId);

            if (pv == null)
            {
                return NotFound();
            }

            // Asetetaan rekisterinumero ViewDataan
            ViewData["PvRekNro"] = pv.RekNro;
            PvHuoltopyyntö.PvId = pvId.Value;

            return Page();

        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var pv = await _context.Pvs.FindAsync(PvHuoltopyyntö.PvId);

            if (pv == null)
            {
                ModelState.AddModelError("PvHuoltopyyntö.HuollonKuvaus", "Ei huoltoa");
                return Page();
            }

            _context.PvHuoltopyyntös.Add(PvHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
