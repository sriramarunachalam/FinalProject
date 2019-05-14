using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FP2.Models;

namespace FP2.Pages.YourCourses
{
    public class EditModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public EditModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Enrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student).FirstOrDefaultAsync(m => m.EnrollmentId == id);

            if (Enrollment == null)
            {
                return NotFound();
            }
           ViewData["CourseId"] = new SelectList(_context.Course, "CourseId", "CourseId");
           ViewData["StudentId"] = new SelectList(_context.Student, "StudentId", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(Enrollment.EnrollmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollment.Any(e => e.EnrollmentId == id);
        }
    }
}
