using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.PvHuolto
{
    public class CreateModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public CreateModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? pvId)
        {
            if (pvId == null)
            {
                return NotFound();
            }

            // Haetaan Pv annettujen pvId perusteella
            var pv = await _context.Pvs.FindAsync(pvId);

            if (pv == null)
            {
                return NotFound();
            }

            // Asetetaan rekisterinumero ViewDataan
            ViewData["PvRekNro"] = pv.RekNro;

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

            // Liitetään PvHuollot-olion PvId valittuun pvId:hen
            PvHuollot = new PvHuollot
            {
                PvId = pv.PvId
            };

            return Page();
        }
        
        [BindProperty]
        public PvHuollot PvHuollot { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Lisää PvHuollot tietokantaan
            var pv = await _context.Pvs.FindAsync(PvHuollot.PvId);
            PvHuollot.Pv = pv;

            if (PvHuollot.Pv == null)
            {
                ModelState.AddModelError("PvHuollot.PvId", "Ei perävaunua ID:llä.");
                return Page();
            }

            _context.PvHuollots.Add(PvHuollot);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
