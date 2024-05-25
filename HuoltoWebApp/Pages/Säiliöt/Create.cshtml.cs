using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

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

        public IActionResult OnGet()
        {
            return Page();
        }

        //Bindpropertyn avulla voidaan sitoa lomakkeelta tulevat tiedot suoraan tähän muuttujaan
        [BindProperty]
        public Säiliö Säiliö { get; set; } = default!; //default! tarkoittaa, että muuttujan arvo ei voi olla null

        // IFormFile on tiedosto, joka on lähetetty lomakkeelta
        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new List<IFormFile>(); // Bindataan lomakkeelta lähetetyt kuvatiedostot tähän listaan
        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new List<IFormFile>(); // Bindataan kuvatiedostot, jotka on otettu kameralla

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Säiliös == null || Säiliö == null)
            {
                return Page();
            }

            //Lisätään säiliö tietokantaan ensin
            _context.Säiliös.Add(Säiliö);
            // Tallennetaan muutokset tietokantaan
            await _context.SaveChangesAsync();

            // Luodaan uusi SäiliöInfo-olio käyttäjän syöttämällä tekstillä
            var säiliöInfo = new SäiliöInfo
            {
                SäiliöId = Säiliö.SäiliöId,
                InfoTxt = Säiliö.InfoTxt
            };

            _context.SäiliöInfos.Add(säiliöInfo);
            await _context.SaveChangesAsync();

            // Käytetään ImageServiceä tallentamaan kuvat
            await _imageService.SaveImagesAsync(Kuvatiedostot, "SäiliöInfo", säiliöInfo.SäiliöInfoId);
            await _imageService.SaveImagesAsync(CapturedImages, "SäiliöInfo", säiliöInfo.SäiliöInfoId);

            return RedirectToPage("./Index");
        }
    }
}
