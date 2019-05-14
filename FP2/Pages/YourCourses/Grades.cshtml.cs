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
    public class GradesModel : PageModel
    {
        private readonly FP2Context _context;

        public GradesModel(FP2Context context)
        {
            _context = context;
        }

        public IList<Enrollment> Enrollment { get; set; }

        public IList<String> Grades { get; set; }

        public double GradePoint { get; set; }
        public double Cgpa = 0.0;
        public int Credits { get; set; }

        public async Task OnGetAsync(long id)
        {
            Enrollment = await _context.Enrollment.Where(e => e.StudentId == id)
                .Include(e=>e.Course)
                .Include(e=>e.Student)
                .OrderBy(e=>e.Course.CourseName)
                .ToListAsync();

            Grades = await _context.Enrollment.Where(e => e.StudentId == id && e.Grade!=null)
                .OrderBy(e=>e.Grade)
                .Include(e=>e.Course)
                .Select(e => e.Grade).ToListAsync();

            Credits = Grades.Count * 3;

            foreach(var item in Grades)
            {
                if (item == "A")
                {
                    Cgpa = Cgpa + ((4.0 * 3)/Credits);                    
                }
                else if (item == "B")
                {
                    Cgpa = Cgpa + ((3.75 * 3) / Credits);
                }
                else if (item == "C")
                {
                    Cgpa = Cgpa + ((3.5 * 3) / Credits);
                }
                else if (item == "D")
                {
                    Cgpa = Cgpa + ((3.25 * 3) / Credits);
                }
                else if (item == "F")
                {
                    Cgpa = Cgpa + ((0.000001 * 3) / Credits);
                }

            }
        }
    }
}