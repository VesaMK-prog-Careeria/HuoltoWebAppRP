using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuollot
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

            // lisätty SäiliöHuolto näkymään Create sivulla
        ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
            return Page();
        }

        [BindProperty]
        public AutoHuollot AutoHuollot { get; set; } = default!;
        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.AutoHuollots == null || AutoHuollot == null || _context.SäiliöHuollots == null || SäiliöHuollot == null)
            {
                return Page();
            }

            // Add SäiliöHuollot to the context and save to generate its primary key
            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();

            // Assign the generated primary key to the foreign key in AutoHuollot
            AutoHuollot.HuollonId = SäiliöHuollot.HuoltoId;

            // Add AutoHuollot to the context and save
            _context.AutoHuollots.Add(AutoHuollot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
