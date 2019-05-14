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
    public class CreateAnnouncementModel : PageModel
    {
        private readonly FP2Context _context;

        public CreateAnnouncementModel(FP2Context context)
        {
            _context = context;
        }

        public Course Course { get; set; }
        public Professor Professor { get; set; }

        [BindProperty]
        public Announcement Announcement { get; set; }

        public async Task<IActionResult> OnGetAsync(string cid, long pid)
        {
            Course = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == cid);
            Professor = await _context.Professor.FirstOrDefaultAsync(p => p.ProfessorId == pid);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string cid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Announcement.Add(Announcement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CoursePage", new { id = cid });
        }
    }
}