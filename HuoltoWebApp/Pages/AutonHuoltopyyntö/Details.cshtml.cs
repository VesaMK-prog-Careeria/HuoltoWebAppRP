﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HuoltoWebApp.Models;
using HuoltoWebApp.Services;

namespace HuoltoWebApp.Pages.AutonHuoltopyyntö
{
    public class DetailsModel : PageModel
    {
        private readonly HuoltoWebApp.Services.HuoltoContext _context;

        public DetailsModel(HuoltoWebApp.Services.HuoltoContext context)
        {
            _context = context;
        }

      public AutoHuoltopyyntö AutoHuoltopyyntö { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.AutoHuoltopyyntös == null)
            {
                return NotFound();
            }

            var autohuoltopyyntö = await _context.AutoHuoltopyyntös.FirstOrDefaultAsync(m => m.HuoltoId == id);
            if (autohuoltopyyntö == null)
            {
                return NotFound();
            }
            else 
            {
                AutoHuoltopyyntö = autohuoltopyyntö;
            }
            return Page();
        }
    }
}
