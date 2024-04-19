using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvHuoltopyynnot
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
        ViewData["PvId"] = new SelectList(_context.Pvs, "PvId", "PvId");
            return Page();
        }

        [BindProperty]
        public PvHuoltopyyntö PvHuoltopyyntö { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PvHuoltopyyntös == null || PvHuoltopyyntö == null)
            {
                return Page();
            }

            _context.PvHuoltopyyntös.Add(PvHuoltopyyntö);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
