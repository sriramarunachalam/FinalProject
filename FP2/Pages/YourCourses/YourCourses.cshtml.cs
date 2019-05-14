using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Identity;

namespace FP2.Pages.YourCourses
{
    public class YourCoursesModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public YourCoursesModel(FP2.Models.FP2Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Enrollment> Enrollment { get;set; }
        public IList<CourseAssignment> CourseAssignment { get; set; }

        public async Task OnGetAsync(long id)
        {
            Enrollment = await _context.Enrollment.Where(e=>e.StudentId==id)
                .Include(e => e.Course)
                .Include(e => e.Student).ToListAsync();

            CourseAssignment = await _context.CourseAssignment
                .Where(c => c.ProfessorId == id)
                .Include(c=>c.Course)
                .Include(c=>c.Professor)
                .ToListAsync();
        }
    }
}
