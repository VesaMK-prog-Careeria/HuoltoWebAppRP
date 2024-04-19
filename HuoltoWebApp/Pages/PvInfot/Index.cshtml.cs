using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.PvInfot
{
    public class IndexModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IndexModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IList<PvInfo> PvInfo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PvInfos != null)
            {
                PvInfo = await _context.PvInfos
                .Include(p => p.Pv).ToListAsync();
            }
        }
    }
}
