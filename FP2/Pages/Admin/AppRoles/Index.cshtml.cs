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
    public class IndexModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public IndexModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<AspNetRoles> AspNetRoles { get;set; }

        public async Task OnGetAsync()
        {
            AspNetRoles = await _context.AspNetRoles.ToListAsync();
        }
    }
}
