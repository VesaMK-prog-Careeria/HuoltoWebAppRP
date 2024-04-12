using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonInfo
{
    public class IndexModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IndexModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IList<AutoInfo> AutoInfo { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.AutoInfos != null)
            {
                AutoInfo = await _context.AutoInfos
                .Include(a => a.Auto).ToListAsync();
            }
        }
    }
}
