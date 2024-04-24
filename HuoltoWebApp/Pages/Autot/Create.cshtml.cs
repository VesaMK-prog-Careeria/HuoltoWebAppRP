using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

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
                AutoId = Auto.AutoId,
                InfoTxt = Auto.InfoTxt
            };

            // Lisätään AutoInfo tietokantaan
            _context.AutoInfos.Add(autoInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        
        }
    }
}
