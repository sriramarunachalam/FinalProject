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
    [Authorize(Roles = "Admin")]
    public class StudentDataDetailsModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public StudentDataDetailsModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student
                .Include(s => s.User)
                .Include(d=>d.Department)
                .FirstOrDefaultAsync(m => m.UserId == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
