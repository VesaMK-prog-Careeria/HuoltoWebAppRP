using HuoltoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.Huollot
{
    public class IlmoitaHuoltoModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IlmoitaHuoltoModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public List<Auto> Autot { get; set; }
        public List<Pv> PVt { get; set; }  // Olettaen että luokan nimi on Peravaunu

        public async Task OnGetAsync()
        {
            Autot = await _context.Autos.ToListAsync();
            PVt = await _context.Pvs.ToListAsync();  // Varmista että tämä vastaa tietokannan rakennetta
        }
    }
}
