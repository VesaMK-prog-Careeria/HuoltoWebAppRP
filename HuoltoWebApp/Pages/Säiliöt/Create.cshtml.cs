using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.Säiliöt
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

        // Olemassa olevat numerot näkymää varten
        public List<int> OlemassaOlevatSäiliöNumerot { get; set; } = new();

        [BindProperty]
        public Säiliö Säiliö { get; set; } = default!;

        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new();

        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            // Haetaan kaikki olemassa olevat numerot
            OlemassaOlevatSäiliöNumerot = await _context.Säiliös
                .OrderBy(s => s.SäiliöNro)
                .Select(s => s.SäiliöNro)
                .ToListAsync();

            // Ehdotetaan seuraavaa numeroa
            int seuraavaNro = (OlemassaOlevatSäiliöNumerot.LastOrDefault()) + 1;

            Säiliö = new Säiliö
            {
                SäiliöNro = seuraavaNro
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Säiliös == null || Säiliö == null)
            {
                // Ladataan numerot uudelleen virheen varalta
                OlemassaOlevatSäiliöNumerot = await _context.Säiliös
                    .OrderBy(s => s.SäiliöNro)
                    .Select(s => s.SäiliöNro)
                    .ToListAsync();

                return Page();
            }

            _context.Säiliös.Add(Säiliö);
            await _context.SaveChangesAsync();

            var säiliöInfo = new SäiliöInfo
            {
                SäiliöId = Säiliö.SäiliöId,
                InfoTxt = Säiliö.InfoTxt
            };

            _context.SäiliöInfos.Add(säiliöInfo);
            await _context.SaveChangesAsync();

            await _imageService.SaveImagesAsync(Kuvatiedostot, "SäiliöInfo", säiliöInfo.SäiliöInfoId);
            await _imageService.SaveImagesAsync(CapturedImages, "SäiliöInfo", säiliöInfo.SäiliöInfoId);

            return RedirectToPage("./Index");
        }
    }
}
