using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvInfot
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
            var pvs = _context.Pvs.ToList();
            ViewData["RekNroList"] = new SelectList(pvs, "PvId", "RekNro");
            return Page();
        }

        [BindProperty]
        public PvInfo PvInfo { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PvInfos == null || PvInfo == null)
            {
                return Page();
            }

            _context.PvInfos.Add(PvInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
