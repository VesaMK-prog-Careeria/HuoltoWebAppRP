﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? autoId)
        {
            if (autoId == null)
            {
                return NotFound();
            }

            // Haetaan Auto annettujen autoId perusteella
            var auto = await _context.Autos.FindAsync(autoId);

            if (auto == null)
            {
                return NotFound();
            }

            // Asetetaan rekisterinumero ViewDataan
            ViewData["AutoRekNro"] = auto.RekNro;

            // Haetaan Huoltopaikat tietokannasta
            var huoltopaikat = await _context.Huoltopaikats.ToListAsync();

            // Tarkistaa, että Huoltopaikat ei ole tyhjä
            if (huoltopaikat != null && huoltopaikat.Any())
            {
                // Luo SelectList Huoltopaikkojen perusteella
                ViewData["Huoltopaikat"] = new SelectList(huoltopaikat, "HuoltoPaikkaId", "Huoltopaikka");
            }
            else
            {
                // Jos Huoltopaikat on tyhjä, luo tyhjä SelectList
                ViewData["Huoltopaikat"] = new SelectList(new List<SelectListItem>());
            }

            // Liitetään AutoHuollot-olion AutoId valittuun autoId:hen
            AutoHuollot = new AutoHuollot
            {
                AutoId = auto.AutoId
            };

            return Page();
        }

        [BindProperty]
        public AutoHuollot AutoHuollot { get; set; } = default!;

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

            if (AutoHuollot.Auto == null)
            {
                ModelState.AddModelError("AutoHuollot.AutoId", "Ei autoa ID:llä.");
                return Page();
            }

            _context.AutoHuollots.Add(AutoHuollot);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
