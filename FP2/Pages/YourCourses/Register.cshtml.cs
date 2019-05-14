using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FP2.Models;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.YourCourses
{
    public class RegisterModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public RegisterModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }
        public Student Student { get; set; }
        public Course Course { get; set; }

        public async Task OnGetAsync(long sid, string cid)
        {
            Student =await _context.Student.FirstOrDefaultAsync(s => s.StudentId == sid);
            Course = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == cid);        
        }

        public async Task<IActionResult> OnPostAsync(string id, long sid, string cid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student = await _context.Student.FirstOrDefaultAsync(s => s.UserId == id);

            var Enrollment = new Enrollment()
            {
                StudentId = sid,
                CourseId = cid
            }; 

            _context.Enrollment.Add(Enrollment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CourseRegistration", new { id = Student.UserId });
        }
    }
}