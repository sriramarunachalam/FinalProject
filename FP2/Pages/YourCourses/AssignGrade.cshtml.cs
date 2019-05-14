using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.YourCourses
{
    [Authorize(Roles ="Professor")]
    public class AssignGradeModel : PageModel
    {
        private readonly FP2Context _context;

        public AssignGradeModel(FP2Context context)
        {
            _context = context;
        }

        public Student Student { get; set; }

       
        public string Grade { get; set; }

        [BindProperty]
        public Enrollment Enrollment { get; set; }

        public async Task OnGetAsync(long sid, string cid)
        {
            Student = await _context.Student.FirstOrDefaultAsync(s => s.StudentId == sid);
            Enrollment = await _context.Enrollment.FirstOrDefaultAsync(e => e.StudentId == sid && e.CourseId == cid);
        }

        public async Task<IActionResult> OnPostAsync(long sid, string cid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enrollment).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Students", new { id = cid });
        }
    }
}