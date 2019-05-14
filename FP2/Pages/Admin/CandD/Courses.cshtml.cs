using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FP2.Pages.Admin.CandD
{
    public class CoursesModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public CoursesModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList Depts {get;set;}

        [BindProperty(SupportsGet = true)]
        public string Department { get; set; }

        public IList<Course> Course { get; set; }        

        public async Task OnGetAsync()
        {
            var courselist = from m in _context.Course
                             select m;

            IQueryable<string> depts = from m in _context.Department
                                              orderby m.DepartmentName
                                              select m.DepartmentName;


            if (!string.IsNullOrEmpty(SearchString))
            {             
                courselist = courselist.Where(c => c.CourseName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Department))
            {
                courselist = courselist.Where(c => c.Department.DepartmentName == Department);
            }

            Depts = new SelectList(await depts.Distinct().ToListAsync());
            Course = await courselist
                .Include(c => c.Department).ToListAsync();
        }
    }
}
