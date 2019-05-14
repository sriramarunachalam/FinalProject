using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.YourCourses
{
    public class GradeModel : PageModel
    {

        private readonly FP2Context _context;

        public GradeModel(FP2Context context)
        {
            _context = context;
        }

        public Enrollment Enrollment { get; set; }

        public async Task OnGetAsync(string cid, long sid)
        {
            Enrollment = await _context.Enrollment
                .Include(e=>e.Course)
                .Include(e=>e.Student)
                .FirstOrDefaultAsync(e => e.StudentId == sid && e.CourseId == cid);

        }
    }
}