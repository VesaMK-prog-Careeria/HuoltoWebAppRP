﻿using System;
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

            // Tarkista ensin, että mallin tila on kelvollinen.
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var auto = await _context.Autos.FindAsync(AutoHuollot.AutoId);
            AutoHuollot.Auto = auto;

            var säiliö = await _context.Säiliös.FindAsync(SäiliöHuollot.SäiliöId);
            SäiliöHuollot.Säiliö = säiliö;

            if (AutoHuollot.Auto == null)
            {
                ModelState.AddModelError("AutoHuollot.AutoId", "Ei autoa ID:llä.");
                return Page();
            }

            if (SäiliöHuollot.Säiliö == null)
            {
                ModelState.AddModelError("SäiliöHuollot.SäiliöId", "Ei säiliötä ID:llä.");
                return Page();
            }

            _context.AutoHuollots.Add(AutoHuollot);
            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
