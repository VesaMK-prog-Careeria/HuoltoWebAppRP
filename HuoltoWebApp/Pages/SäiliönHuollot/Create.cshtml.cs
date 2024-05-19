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

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöNro", "SäiliöNro");

            // Haetaan Huoltopaikat tietokannasta
            var huoltopaikat = await _context.Huoltopaikats.ToListAsync();

            // Tarkista, että Huoltopaikat ei ole tyhjä
            if (huoltopaikat != null && huoltopaikat.Any())
            {
                // Luo SelectList Huoltopaikkojen perusteella
                ViewData["Huoltopaikat"] = new SelectList(huoltopaikat, "HuoltoPaikkaId", "Huoltopaikka");
            }
            else
            {
                // Jos Huoltopaikat on tyhjä, luo tyhjä SelectList
                ViewData["Huoltopaikat"] = new SelectList(new List<SelectListItem>());
            }

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