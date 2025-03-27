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

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoContext _context;
        private readonly ImageService _imageService;

        public CreateModel(HuoltoContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new();

        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new();

        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;

        public Auto Auto { get; set; }  // Lisätään Auto-olio, jotta se voidaan näyttää Razor-sivulla

        public async Task<IActionResult> OnGetAsync(int? autoId)
        {
            if (autoId == null || autoId == 0)
            {
                return NotFound("AutoId puuttuu tai on virheellinen.");
            }

            // Haetaan auto ja siihen liitetty säiliö
            Auto = await _context.Autos
                .Include(a => a.Säiliö) // Liittää säiliön autoon
                .FirstOrDefaultAsync(a => a.AutoId == autoId);

            if (Auto == null)
            {
                return NotFound($"Autoa ID:llä {autoId} ei löytynyt.");
            }

            if (Auto.Säiliö == null)
            {
                return NotFound($"Autolle (ID: {autoId}) ei ole liitettyä säiliötä.");
            }

            // Debug-tulostus varmistamaan haettu data
            Console.WriteLine($"Auto ID: {Auto.AutoId}, Rekisterinumero: {Auto.RekNro}, Liitetty Säiliö ID: {Auto.Säiliö.SäiliöId}, SäiliöNro: {Auto.Säiliö.SäiliöNro}");

            // Asetetaan rekisterinumero ViewDataan
            ViewData["AutoRekNro"] = Auto.RekNro;

            // Haetaan vain kyseiseen autoon liitetty Säiliö ja lisätään valintaan
            ViewData["SäiliöId"] = new SelectList(
                new List<Säiliö> { Auto.Säiliö },
                "SäiliöId", "SäiliöNro"
            );

            // Haetaan huoltopaikat tietokannasta
            var huoltopaikat = await _context.Huoltopaikats.ToListAsync();

            ViewData["Huoltopaikat"] = huoltopaikat.Any()
                ? new SelectList(huoltopaikat, "HuoltoPaikkaId", "Huoltopaikka")
                : new SelectList(new List<SelectListItem>());

            // Liitetään AutoHuollot-olion AutoId valittuun autoId:hen
            AutoHuollot = new AutoHuollot
            {
                AutoId = Auto.AutoId
            };

            return Page();
        }


        [BindProperty]
        public AutoHuollot AutoHuollot { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            // Tarkista ensin, että mallin tila on kelvollinen.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var auto = await _context.Autos.FindAsync(AutoHuollot.AutoId);
            AutoHuollot.Auto = auto;

            if (AutoHuollot.Auto == null)
            {
                ModelState.AddModelError("AutoHuollot.AutoId", "Ei autoa ID:llä.");
                return Page();
            }

            _context.AutoHuollots.Add(AutoHuollot);
            await _context.SaveChangesAsync();

            await _imageService.SaveImagesAsync(Kuvatiedostot, "AutoHuollot", AutoHuollot.HuollonId);
            await _imageService.SaveImagesAsync(CapturedImages, "AutoHuollot", AutoHuollot.HuollonId);

            return RedirectToPage("./Index");
        }
    }
}
