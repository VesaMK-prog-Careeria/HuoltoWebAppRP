using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Säiliöt
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
            return Page();
        }

        [BindProperty]
        public Säiliö Säiliö { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Säiliös == null || Säiliö == null)
            {
                return Page();
            }

            _context.Säiliös.Add(Säiliö);
            await _context.SaveChangesAsync();

            var säiliöInfo = new SäiliöInfo
            {
                SäiliöId = Säiliö.SäiliöId,
                InfoTxt = Säiliö.InfoTxt

            };

            _context.SäiliöInfos.Add(säiliöInfo);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
