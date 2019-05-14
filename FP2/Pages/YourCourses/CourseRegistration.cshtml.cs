using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;

namespace FP2.Pages.YourCourses
{
    public class CourseRegistrationModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public CourseRegistrationModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<Course> Course { get;set; }
        public Student Student { get; set; }

        public IList<Enrollment> Enrollment { get; set; }

        public List<string> courses = new List<string>();

        public IList<CourseAssignment> CourseAssignment { get; set; }

        public async Task OnGetAsync(string id)
        {
            Student = await _context.Student.FirstOrDefaultAsync(s => s.UserId == id);

            Enrollment = await _context.Enrollment.Where(e => e.StudentId == Student.StudentId).ToListAsync();

            foreach(var i in Enrollment)
            {
                courses.Add(i.CourseId);
            }

            foreach(var item in courses)
            {
                CourseAssignment= await _context.CourseAssignment
                    .OrderBy(c=>c.CourseId)
                    .Include(c => c.Professor)
                    .Include(c=>c.Course)
                    .ToListAsync();
            }

            Course = await _context.Course
                .Include(c => c.Department).ToListAsync();
        }
    }
}
