using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.YourCourses
{
    public class CoursePageModel : PageModel
    {
        private readonly FP2Context _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoursePageModel(FP2Context context, UserManager<IdentityUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        public Course Course { get; set; }
        public string userid { get; set; }
        public Professor Professor { get; set; }
        public Student Student { get; set; }

        public IList<Announcement> Announcements { get; set; }

        public async Task OnGetAsync(string id)
        {
            userid = _userManager.GetUserId(HttpContext.User);
            Course = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == id);
            Professor = await _context.Professor.FirstOrDefaultAsync(p => p.UserId == userid);
            Student = await _context.Student.FirstOrDefaultAsync(s => s.UserId == userid);

            Announcements = await _context.Announcement.Where(a => a.CourseId == id).OrderByDescending(a=>a.Date).ToListAsync();
        }
    }
}