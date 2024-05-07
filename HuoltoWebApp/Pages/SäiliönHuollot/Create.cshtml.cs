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

namespace HuoltoWebApp.Pages.SäiliönHuollot
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
            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
            return Page();
        }

        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Säiliö");
            // Tarkista ensin, että mallin tila on kelvollinen.
            if (!ModelState.IsValid)
            {
                _logger.LogInformation("ModelState is not valid.");
                return Page();
            }

            // Haetaan säiliö tietokannasta.
            SäiliöHuollot.Säiliö = await _context.Säiliös.FindAsync(SäiliöHuollot.SäiliöId);

            // Tarkista, onko säiliö löytynyt.
            if (SäiliöHuollot.Säiliö == null)
            {
                _logger.LogInformation($"No valid tank found for the given ID: {SäiliöHuollot.SäiliöId}");
                ModelState.AddModelError("SäiliöHuollot.SäiliöId", "Valittu säiliö ei ole voimassa.");
                return Page();
            }

            // Kaikki tarkistukset ovat menneet läpi, lisätään huolto tietokantaan.
            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();
            _logger.LogInformation("SäiliöHuollot was created successfully.");

            // Siirrytään takaisin pääsivulle onnistuneen tallennuksen jälkeen.
            return RedirectToPage("./Index");
        }
    }
}