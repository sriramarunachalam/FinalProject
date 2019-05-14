using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles ="Admin")]
    public class ProfessorDataDetailsModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public ProfessorDataDetailsModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public Professor Professor { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _context.Professor
                .Include(p => p.Department)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (Professor == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
