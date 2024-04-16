using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.SäiliönHuoltopyyntö
{
    public class IndexModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public IndexModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

        public IList<SäiliöHuoltopyyntö> SäiliöHuoltopyyntö { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SäiliöHuoltopyyntös != null)
            {
                SäiliöHuoltopyyntö = await _context.SäiliöHuoltopyyntös
                .Include(s => s.Säiliö).ToListAsync();
            }
        }
    }
}
