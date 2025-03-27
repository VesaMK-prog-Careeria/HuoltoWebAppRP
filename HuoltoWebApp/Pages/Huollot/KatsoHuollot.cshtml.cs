using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HuoltoWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.Huollot
{
    public class KatsoHuollotModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public KatsoHuollotModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public List<Auto>? Autot { get; set; }
        public List<Pv>? PVt { get; set; }
        public List<S�ili�>? S�ili�t { get; set; }
        public async Task OnGetAsync()
        {
            Autot = await _context.Autos.ToListAsync();
            PVt = await _context.Pvs.ToListAsync();
            S�ili�t = await _context.S�ili�s.ToListAsync();
        }
    }
}
