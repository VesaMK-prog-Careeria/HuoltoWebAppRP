using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonInfo
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
            var test = _context.AutoInfos.ToList();
        }

      public AutoInfo AutoInfo { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //System.Diagnostics.Debug.WriteLine($"ID: {id}");
            if (id == null || _context.AutoInfos == null)
            {
                return NotFound();
            }

            var autoinfo = await _context.AutoInfos
                .Include(ai => ai.Kuvat)
                .FirstOrDefaultAsync(m => m.AutoInfoId == id);

            if (autoinfo == null)
            {
                return NotFound();
            }
            else 
            {
                AutoInfo = autoinfo;
            }
            return Page();
        }
        public IActionResult OnGetImage(int id)
        {
            var kuva = _context.Kuvat.FirstOrDefault(k => k.KuvaId == id);
            if (kuva == null || kuva.KuvaData == null)
            {
                return NotFound("Kuva tai kuvadata ei löydy.");
            }

            return File(kuva.KuvaData, "image/jpeg"); // Varmista, että MIME-tyyppi vastaa kuvatiedoston tyyppiä
        }
    }
}
