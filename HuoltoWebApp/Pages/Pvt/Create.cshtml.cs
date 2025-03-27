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

namespace HuoltoWebApp.Pages.Pvt
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

        [BindProperty]
        public Pv Pv { get; set; } = default!;

        // IFormFile on tiedosto, joka on lähetetty lomakkeelta
        [BindProperty]
        public List<IFormFile> Kuvatiedostot { get; set; } = new List<IFormFile>(); // Bindataan lomakkeelta lähetetyt kuvatiedostot tähän listaan
        [BindProperty]
        public List<IFormFile> CapturedImages { get; set; } = new List<IFormFile>(); // Bindataan kuvatiedostot, jotka on otettu kameralla

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Pvs == null || Pv == null)
            {
                return Page();
            }

            // Lisätään Pv tietokantaan ensin
            _context.Pvs.Add(Pv);
            await _context.SaveChangesAsync();

            // Debuggausta varten
            Console.WriteLine($"Tarkistus: Pv.InfoTxt = {Pv.InfoTxt}");

            // Luodaan uusi PvInfo-olio käyttäjän syöttämällä tekstillä
            var pvInfo = new PvInfo
            {
                PvId = Pv.PvId,
                InfoTxt = Pv.InfoTxt
            };

            // Lisätään PvInfo tietokantaan
            _context.PvInfos.Add(pvInfo);
            await _context.SaveChangesAsync();

            //Pv.PvInfo = pvInfo;
            //await _context.SaveChangesAsync();

            // Käytetään ImageServiceä tallentamaan kuvat
            await _imageService.SaveImagesAsync(Kuvatiedostot, "PvInfo", pvInfo.PvInfoId);
            await _imageService.SaveImagesAsync(CapturedImages, "PvInfo", pvInfo.PvInfoId);

            return RedirectToPage("./Index");
        }
    }
}
