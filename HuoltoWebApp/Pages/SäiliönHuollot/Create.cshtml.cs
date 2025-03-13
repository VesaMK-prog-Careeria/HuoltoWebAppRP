using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.SäiliönHuollot
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int autoId)
        {
            var auto = await _context.Autos
                .Include(a => a.Säiliö) // Liittää säiliön autoon
                .FirstOrDefaultAsync(a => a.AutoId == autoId);

            if (auto == null)
            {
                return NotFound($"Autoa ID:llä {autoId} ei löytynyt.");
            }

            if (auto.Säiliö == null)
            {
                return NotFound($"Autolle (ID: {autoId}) ei ole liitettyä säiliötä.");
            }

            // Näytetään vain kyseiseen autoon liittyvä säiliö
            ViewData["SäiliöId"] = new SelectList(
                new List<Säiliö> { auto.Säiliö },
                "SäiliöId", "SäiliöNro"
            );

            // Haetaan Huoltopaikat tietokannasta
            var huoltopaikat = await _context.Huoltopaikats.ToListAsync();
            ViewData["Huoltopaikat"] = huoltopaikat.Any()
                ? new SelectList(huoltopaikat, "HuoltoPaikkaId", "Huoltopaikka")
                : new SelectList(new List<SelectListItem>());

            return Page();
        }

        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var säiliö = await _context.Säiliös.FindAsync(SäiliöHuollot.SäiliöId);
            SäiliöHuollot.Säiliö = säiliö;

            if (SäiliöHuollot.Säiliö == null)
            {
                ModelState.AddModelError("", "Ei säiliöitä ID:llä.");
                return Page();
            }

            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}