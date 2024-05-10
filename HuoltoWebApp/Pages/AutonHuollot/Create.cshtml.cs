using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.Extensions.Logging;

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
        ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoId");

            // lisätty SäiliöHuolto näkymään Create sivulla
        ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
            return Page();
        }

        [BindProperty]
        public AutoHuollot AutoHuollot { get; set; } = default!;
        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Auto");
            ModelState.Remove("Säiliö");

            // Tarkista ensin, että mallin tila on kelvollinen.
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid.");
                return Page();
            }

            // Haetaan auto ja säiliö tietokannasta.
            AutoHuollot.Auto = await _context.Autos.FindAsync(AutoHuollot.AutoId) ?? throw new ArgumentNullException(nameof(AutoHuollot.Auto));
            SäiliöHuollot.Säiliö = await _context.Säiliös.FindAsync(SäiliöHuollot.SäiliöId) ?? throw new ArgumentNullException(nameof(SäiliöHuollot.Säiliö));

            // Tarkista, onko auto ja säiliö löytynyt.
            if (AutoHuollot.Auto == null)
            {
                _logger.LogInformation($"No valid car found for the given ID: {AutoHuollot.AutoId}");
                ModelState.AddModelError("AutoHuollot.AutoId", "Valittu auto ei ole voimassa.");
                return Page();
            }

            if (SäiliöHuollot.Säiliö == null)
            {
                _logger.LogInformation($"No valid tank found for the given ID: {SäiliöHuollot.SäiliöId}");
                ModelState.AddModelError("SäiliöHuollot.SäiliöId", "Valittu säiliö ei ole voimassa.");
                return Page();
            }

            // Kaikki tarkistukset ovat menneet läpi, lisätään huolto tietokantaan.
            _context.AutoHuollots.Add(AutoHuollot);
            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();
            _logger.LogInformation("AutoHuollot and SäiliöHuollot were created successfully.");

            // Siirrytään takaisin pääsivulle onnistuneen tallennuksen jälkeen.
            return RedirectToPage("./Index");
        }
    }
}
