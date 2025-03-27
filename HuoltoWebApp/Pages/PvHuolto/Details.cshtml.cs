using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvHuolto
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

        public PvHuollot PvHuollot { get; set; } = default!;
        public List<Kuva> Kuvat { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PvHuollots == null)
            {
                return NotFound();
            }

            var pvhuollot = await _context.PvHuollots.FirstOrDefaultAsync(m => m.HuoltoId == id);
            
            if (pvhuollot == null)
            {
                return NotFound();
            }
            else 
            {
                PvHuollot = pvhuollot;
            }

            //Haetaan kuvat
            Kuvat = await _imageService.GetKuvatAsync("PvHuollot", pvhuollot.HuoltoId);

            return Page();
        }
    }
}
