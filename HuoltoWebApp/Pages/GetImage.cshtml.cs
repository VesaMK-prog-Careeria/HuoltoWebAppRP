using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuoltoWebApp.Pages
{
    public class GetImageModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public GetImageModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
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

