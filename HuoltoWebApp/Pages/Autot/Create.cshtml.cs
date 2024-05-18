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
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöNro");
            return Page();
        }

        [BindProperty]
        public Auto Auto { get; set; } = default!;
        [BindProperty]
        public List<IFormFile>? Kuvatiedostot { get; set; } = new List<IFormFile>(); // Bindataan lomakkeelta lähetetyt kuvatiedostot tähän listaan


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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

            foreach (var tiedosto in Kuvatiedostot)
            {
                if (tiedosto.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await tiedosto.CopyToAsync(ms);
                    if (ms.Length > 0) // Varmistetaan, että data on kopioitu
                    {
                        var kuva = new Kuva()
                        {
                            KuvaNimi = tiedosto.FileName,
                            KuvaData = ms.ToArray(), // Muutetaan tiedosto byte-taulukoksi
                            AutoInfoId = autoInfo.AutoInfoId // Use the AutoInfoId from the autoInfo object
                        };
                        _context.Kuvat.Add(kuva);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
