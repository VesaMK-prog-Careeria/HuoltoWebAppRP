using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.Muistutustyypit
{
    public class IndexModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IndexModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IList<Muistutustyyppi> Muistutustyyppi { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Muistutustyyppis != null)
            {
                Muistutustyyppi = await _context.Muistutustyyppis.ToListAsync();
            }
        }
    }
}
