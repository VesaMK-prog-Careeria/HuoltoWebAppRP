using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuollot
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoContext _context;
        private readonly ImageService _imageService;

        public DetailsModel(HuoltoContext context, ImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        public AutoHuollot AutoHuollot { get; set; } = default!;
        public List<Kuva> Kuvat { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoHuollots == null)
            {
                return NotFound();
            }

            var autohuollot = await _context.AutoHuollots.FirstOrDefaultAsync(m => m.HuollonId == id);
            if (autohuollot == null)
            {
                return NotFound();
            }
            else 
            {
                AutoHuollot = autohuollot;
            }

            //Haetaan kuvat
            Kuvat = await _imageService.GetKuvatAsync("AutoHuollot", autohuollot.HuollonId);
            return Page();
        }
    }
}
