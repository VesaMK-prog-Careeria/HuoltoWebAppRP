using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using System.IO;

namespace HuoltoWebApp.Pages.Autot
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
            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöNro");
            return Page();
        }

        [BindProperty]
        public Auto Auto { get; set; } = default!;
        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new List<IFormFile>(); // Bindataan lomakkeelta lähetetyt kuvatiedostot tähän listaan
        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new List<IFormFile>(); // Bindataan kuvatiedostot, jotka on otettu kameralla

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Autos == null || Auto == null)
            {
                return Page();
            }

            //Lisätään auto tietokantaan ensin
            _context.Autos.Add(Auto);
            await _context.SaveChangesAsync();

            // Luodaan uusi AutoInfo-olio käyttäjän syöttämällä tekstillä
            var autoInfo = new AutoInfo
            {
                InfoTxt = Auto.InfoTxt,
                AutoId = Auto.AutoId
            };
            _context.AutoInfos.Add(autoInfo);
            await _context.SaveChangesAsync();  // Varmista, että tämä tallennus tapahtuu ennen kuin käytät AutoInfoId:tä

            Auto.AutoInfo = autoInfo;  // Tämä rivi varmistaa, että AutoInfo on liitetty Autoon
            await _context.SaveChangesAsync();

            // Käytetään ImageServiceä tallentamaan kuvat
            await _imageService.SaveImagesAsync(Kuvatiedostot, "AutoInfo", autoInfo.AutoInfoId);
            await _imageService.SaveImagesAsync(CapturedImages, "AutoInfo", autoInfo.AutoInfoId);

            return RedirectToPage("./Index");
        }
    }
}
