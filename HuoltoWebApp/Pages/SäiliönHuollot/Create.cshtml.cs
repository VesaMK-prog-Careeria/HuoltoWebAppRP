using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönHuollot
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
            ViewData["SäiliöId"] = new SelectList(_context.Säiliös, "SäiliöId", "SäiliöId");
            return Page();
        }

        [BindProperty]
        public SäiliöHuollot SäiliöHuollot { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var säiliö = await _context.Säiliös.FindAsync(SäiliöHuollot.SäiliöId);
            SäiliöHuollot.Säiliö = säiliö;

            if (SäiliöHuollot.Säiliö == null)
            {
                ModelState.AddModelError("", "Ei säiliöitä ID:llä.");
                return Page();
            }

            _context.SäiliöHuollots.Add(SäiliöHuollot);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}