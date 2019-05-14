using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;

namespace FP2.Pages.Admin.AppRoles
{
    public class DetailsModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public DetailsModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public AspNetRoles AspNetRoles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetRoles = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetRoles == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
