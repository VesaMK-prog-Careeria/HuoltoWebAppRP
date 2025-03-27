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
        public List<Säiliö>? Säiliöt { get; set; }
        public async Task OnGetAsync()
        {
            Autot = await _context.Autos.ToListAsync();
            PVt = await _context.Pvs.ToListAsync();
            Säiliöt = await _context.Säiliös.ToListAsync();
        }
    }
}
