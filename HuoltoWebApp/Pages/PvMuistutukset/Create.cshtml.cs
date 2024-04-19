﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvMuistutukset
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
        ViewData["Muistutustyyppi"] = new SelectList(_context.Muistutustyyppis, "MuistutustyyppiId", "MuistutustyyppiId");
        ViewData["PvId"] = new SelectList(_context.Pvs, "PvId", "PvId");
            return Page();
        }

        [BindProperty]
        public PvMuistutu PvMuistutu { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.PvMuistutus == null || PvMuistutu == null)
            {
                return Page();
            }

            _context.PvMuistutus.Add(PvMuistutu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
