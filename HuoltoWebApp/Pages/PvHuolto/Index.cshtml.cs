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
    public class IndexModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IndexModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IList<PvHuollot> PvHuollot { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PvHuollots != null)
            {
                PvHuollot = await _context.PvHuollots
                .Include(p => p.Pv).ToListAsync();
            }
        }
    }
}
