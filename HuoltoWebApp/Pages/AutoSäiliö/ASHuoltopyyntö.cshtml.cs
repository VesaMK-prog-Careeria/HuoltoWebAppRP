using HuoltoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace HuoltoWebApp.Pages.AutoSäiliö //VK
{
    public class ASHuoltopyyntöModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public ASHuoltopyyntöModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        // AutoHuoltopyyntö- ja SäiliöHuoltopyyntö-olioiden alustus jotta niitä voidaan käyttää sivulla
        [BindProperty] // BindProperty-ominaisuus mahdollistaa tietojen siirtämisen sivulta modeliin eli AutoHuoltopyyntö-olioon
        public AutoHuoltopyyntö AutoHuoltopyyntö { get; set; } = new AutoHuoltopyyntö();
        [BindProperty]
        public SäiliöHuoltopyyntö SäiliöHuoltopyyntö { get; set; } = new SäiliöHuoltopyyntö();
        public Säiliö Säiliö { get; set; }

        // OnGetAsync-metodi hakee Auto- ja Säiliö-oliot annetun autoId:n perusteella (sivun latauslogiikka)
        public async Task<IActionResult> OnGetAsync(int? autoId)
        {
            if (autoId == null)
            {
                return NotFound();
            }

            // Haetaan Auto annettujen autoId perusteella ja liitetään siihen Säiliö
            var auto = await _context.Autos
                                     .Include(a => a.Säiliö)
                                     .FirstOrDefaultAsync(a => a.AutoId == autoId);

            if (auto == null)
            {
                return NotFound();
            }

            // Asetetaan rekisterinumero ViewDataan ja AutoId AutoHuoltopyyntöön
            ViewData["AutoRekNro"] = auto.RekNro;
            AutoHuoltopyyntö.AutoId = autoId.Value;

            // Asetetaan SäiliöId SäiliöHuoltopyyntöön, jos Säiliö löytyy
            if (auto.Säiliö != null)
            {
                Säiliö = auto.Säiliö;
                SäiliöHuoltopyyntö.SäiliöId = Säiliö.SäiliöId;
            }

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAutoAsync()
        {
            ModelState.Remove("AutoHuoltopyyntö.Auto");
            if (!TryValidateModel(AutoHuoltopyyntö, nameof(AutoHuoltopyyntö)))
            {
                return Page();
            }

            var auto = await _context.Autos.FindAsync(AutoHuoltopyyntö.AutoId);
            if (auto == null)
            {
                ModelState.AddModelError("AutoHuoltopyyntö.AutoId", "AutoId is invalid.");
                return Page();
            }

            AutoHuoltopyyntö.Auto = auto;

            _context.AutoHuoltopyyntös.Add(AutoHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Etusivu");
        }

        public async Task<IActionResult> OnPostSäiliöAsync()
        {
            ModelState.Remove("SäiliöHuoltopyyntö.Säiliö");
            if (!TryValidateModel(SäiliöHuoltopyyntö, nameof(SäiliöHuoltopyyntö)))
            {
                return Page();
            }

            var säiliö = await _context.Säiliös.FindAsync(SäiliöHuoltopyyntö.SäiliöId);
            if (säiliö == null)
            {
                ModelState.AddModelError("SäiliöHuoltopyyntö.SäiliöId", "SäiliöId is invalid.");
                return Page();
            }

            SäiliöHuoltopyyntö.Säiliö = säiliö;

            _context.SäiliöHuoltopyyntös.Add(SäiliöHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Etusivu");
        }
    }
}