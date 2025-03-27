using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;
using HuoltoWebApp.Migrations;


namespace HuoltoWebApp.Pages.SäiliönHuollot
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

        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;
        public List<Kuva> Kuvat { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SäiliöHuollots == null)
            {
                return NotFound();
            }

            var säiliöhuollot = await _context.SäiliöHuollots
                .Include(s => s.Säiliö)
                .FirstOrDefaultAsync(m => m.HuoltoId == id);

            if (säiliöhuollot == null)
            {
                return NotFound();
            }
            else 
            {
                SäiliöHuollot = säiliöhuollot;
            }

            //Haetaan kuvat
            Kuvat = await _imageService.GetKuvatAsync("SäiliöHuollot", säiliöhuollot.HuoltoId);

            return Page();
        }
    }
}
