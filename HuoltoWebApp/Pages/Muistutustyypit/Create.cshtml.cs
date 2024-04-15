using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Muistutustyypit
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
            return Page();
        }

        [BindProperty]
        public Muistutustyyppi Muistutustyyppi { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Muistutustyyppis == null || Muistutustyyppi == null)
            {
                return Page();
            }

            _context.Muistutustyyppis.Add(Muistutustyyppi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
