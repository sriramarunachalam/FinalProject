using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles = "Admin")]
    public class StudentsInfoModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public StudentsInfoModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }
      

        public async Task OnGetAsync()
        {
            
            Student = await _context.Student
               .Include(p => p.Department)
               .Include(p => p.User)
               .OrderBy(p=>p.LastName)
               .ToListAsync();
        }
    }
}
