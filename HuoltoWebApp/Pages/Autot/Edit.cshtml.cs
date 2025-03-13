using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Autot
{
    public class EditModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public EditModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Auto Auto { get; set; } = default!;
        [BindProperty]
        public List<IFormFile>? Kuvatiedostot { get; set; } = new List<IFormFile>(); // Bindataan lomakkeelta lähetetyt kuvatiedostot tähän listaan


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            Auto = await _context.Autos
                .Include(a => a.AutoInfo) // Ladataan AutoInfo, mutta se voi olla null
                .FirstOrDefaultAsync(m => m.AutoId == id);

            if (Auto == null)
            {
                return NotFound();
            }

            // Varmistetaan, että AutoInfo voi olla tyhjä ilman virhettä käyttöliittymässä
            if (Auto.AutoInfo == null)
            {
                Auto.AutoInfo = new AutoInfo(); // Luodaan tyhjä olio, jotta käyttöliittymässä ei tule null-viittausvirhettä
            }

            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Auto).State = EntityState.Modified; // Tämä rivi kertoo EF:lle, että Auto-olio on muuttunut ja se pitäisi päivittää tietokantaan

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoExists(Auto.AutoId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            if (Auto.AutoInfo == null)
            {
                Auto.AutoInfo = await _context.AutoInfos.FirstOrDefaultAsync(ai => ai.AutoId == Auto.AutoId);
                if (Auto.AutoInfo == null)
                {
                    return NotFound("AutoInfo not found for the provided Auto.");
                }
            }

            // Kuvien käsittely
            if (Kuvatiedostot != null && Kuvatiedostot.Any())
            {
                foreach (var tiedosto in Kuvatiedostot)
                {
                    if (tiedosto.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        await tiedosto.CopyToAsync(ms);
                        if (ms.Length > 0)
                        {
                            var kuva = new Kuva()
                            {
                                KuvaNimi = tiedosto.FileName,
                                KuvaData = ms.ToArray(),
                                AutoInfoId = Auto.AutoInfo.AutoInfoId
                            };
                            _context.Kuvat.Add(kuva);
                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }

        private bool AutoExists(int id)
        {
          return (_context.Autos?.Any(e => e.AutoId == id)).GetValueOrDefault();
        }
    }
}
