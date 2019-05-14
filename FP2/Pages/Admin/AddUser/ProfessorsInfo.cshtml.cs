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
    public class ProfessorsInfoModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public ProfessorsInfoModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<Professor> Professor { get;set; }
        public IList<CourseAssignment> CourseAssignments { get; set; }
        public List<long> professorids = new List<long>();
 
        public async Task OnGetAsync(long? id)
        {
            Professor = await _context.Professor
                .Include(p => p.Department)
                .Include(p => p.User).ToListAsync();

            CourseAssignments = await _context.CourseAssignment.ToListAsync();
            foreach(var i in CourseAssignments)
            {
                professorids.Add(i.ProfessorId);
            }
        }
    }
}
