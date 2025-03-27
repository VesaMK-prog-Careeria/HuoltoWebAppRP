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
        private readonly HuoltoContext _context;
        private readonly ImageService _imageService;

        //private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new();

        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new();

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
            //Debuggausta varten
            //foreach (var kvp in ModelState)
            //{
            //    foreach (var error in kvp.Value.Errors)
            //    {
            //        Console.WriteLine($"ModelState Error: {kvp.Key} => {error.ErrorMessage}");
            //    }
            //}

            ModelState.Remove("Kuvatiedostot");
            ModelState.Remove("CapturedImages");

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

            // Tallenna kuvat liittyen SäiliöHuollot
            await _imageService.SaveImagesAsync(Kuvatiedostot, "SäiliöHuollot", SäiliöHuollot.HuoltoId);
            await _imageService.SaveImagesAsync(CapturedImages, "SäiliöHuollot", SäiliöHuollot.HuoltoId);

            return RedirectToPage("./Index");
        }
    }
}