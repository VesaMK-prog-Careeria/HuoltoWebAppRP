using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonMuistutus
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
        ViewData["AutoId"] = new SelectList(_context.Autos, "AutoId", "AutoId");
        ViewData["Muistutustyyppi"] = new SelectList(_context.Muistutustyyppis, "MuistutustyyppiId", "MuistutustyyppiId");
            return Page();
        }

        [BindProperty]
        public AutoMuistutu AutoMuistutu { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AutoMuistutus == null || AutoMuistutu == null)
            {
                return Page();
            }

            _context.AutoMuistutus.Add(AutoMuistutu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
