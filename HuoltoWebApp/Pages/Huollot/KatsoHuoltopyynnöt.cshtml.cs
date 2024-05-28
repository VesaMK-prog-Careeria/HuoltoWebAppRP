using HuoltoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HuoltoWebApp.Pages.Huollot
{
    public class KatsoHuoltopyynnötModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public KatsoHuoltopyynnötModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public List<Auto>? Autos { get; set; }
        public List<Säiliö>? Säiliös { get; set; }
        public List<Pv>? Pvs { get; set; }

        public async Task OnGetAsync()
        {
            Autos = await _context.Autos
                .Include(a => a.AutoHuoltopyyntös)
                .ToListAsync();
            Säiliös = await _context.Säiliös
                .Include(s => s.SäiliöHuoltopyyntös)
                .ToListAsync();
            Pvs = await _context.Pvs
                .Include(Pvs => Pvs.PvHuoltopyyntös)
                .ToListAsync();
        }
    }
}
