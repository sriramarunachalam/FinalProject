using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles ="Admin")]
    public class CourseAssignmentModel : ProfessorCoursesPageModel
    {
        private readonly FP2Context _context;

        public CourseAssignmentModel(FP2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Professor Professor { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professor = await _context.Professor
                .Include(p => p.CourseAssignment).ThenInclude(p => p.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProfessorId == id);

            if (Professor == null)
            {
                return NotFound();

            }


            PopulateAssignedCourseData(_context, Professor);
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(long? id, string[] selectedCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var professorToUpdate = await _context.Professor
                .Include(p => p.CourseAssignment).ThenInclude(p => p.Course)
                .FirstOrDefaultAsync(s => s.ProfessorId == id); ;
                
            if(await TryUpdateModelAsync<Professor>(
                professorToUpdate,
                "Professor",
                i=>i.FirstName, i=>i.LastName
                ))
            {
                UpdateProfessorCourses(_context, selectedCourses, professorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./ProfessorsInfo");
            }

            UpdateProfessorCourses(_context, selectedCourses, professorToUpdate);
            PopulateAssignedCourseData(_context, professorToUpdate);
            return Page();
        }
    }
}