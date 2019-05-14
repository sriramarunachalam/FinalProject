using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles = "Admin")]
    public class StudentUsersModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;
        public readonly UserManager<IdentityUser> _userManager;

        public StudentUsersModel(FP2.Models.FP2Context context, UserManager<IdentityUser> userManager )
        {
            _context = context;
            _userManager = userManager;
        }
        
        public IList<IdentityUser> studentUsers { get;set; }

        public IList<AspNetUsers> Users { get; set; }

        public IList<Student> Student { get; set; }

        public List<string> userids = new List<string>();

        public async Task OnGetAsync(string id)
        {
            //Users = await _context.AspNetUsers.OrderBy(u => u.UserName).ToListAsync();

            studentUsers = await _userManager.GetUsersInRoleAsync("Student");
            studentUsers = studentUsers.OrderBy(i => i.UserName).ToList();

            Student = await _context.Student.ToListAsync();

            foreach(var i in Student)
            {
                userids.Add(i.UserId);
            }

            
            

                
        }
    }
}
