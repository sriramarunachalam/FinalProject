using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;

namespace FP2.Pages.Admin.CandD
{
    public class DepartmentsModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public DepartmentsModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            Department = await _context.Department.ToListAsync();
        }
    }
}
