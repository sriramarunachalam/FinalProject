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
    public class StudentsModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;
        

        public StudentsModel(FP2.Models.FP2Context context)
        {
            _context = context;          
        }

        public IList<Enrollment> Enrollment { get;set; }
        public Course Course { get; set; }

        public List<string> userids = new List<string>();
        public IList<string> Emails { get; set; }

        public List<string> emailstest = new List<string>();

        public int count { get; set; }
        public IList<AspNetUsers> AspNetUser { get; set; }
        
        public async Task OnGetAsync(string id)
        {

            Enrollment = await _context.Enrollment
                .Where(e=>e.CourseId==id)
                .Include(e => e.Course)
                .Include(e => e.Student).ToListAsync();            

            for(var i = 0; i < Enrollment.Count; i++)
            {
                userids.Add(Enrollment[i].Student.UserId);
            }
           
            foreach (var item in userids)
            {
                Emails = await _context.AspNetUsers.Where(a => a.Id == item).Select(s => s.UserName).ToListAsync();
                foreach(var i in Emails)
                {
                    emailstest.Add(i);
                }
            }

            Course = await _context.Course.FirstOrDefaultAsync(c => c.CourseId == id);
        }
    }
}
