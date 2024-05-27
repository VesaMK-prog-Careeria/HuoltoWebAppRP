using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuoltopyyntö
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
            return Page();
        }

        [BindProperty]
        public AutoHuoltopyyntö AutoHuoltopyyntö { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.AutoHuoltopyyntös == null || AutoHuoltopyyntö == null)
            {
                return Page();
            }

            _context.AutoHuoltopyyntös.Add(AutoHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
