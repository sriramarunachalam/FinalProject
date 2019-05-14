using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.CandD
{
    [Authorize(Roles ="Admin")]
    public class AddCourseModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public AddCourseModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DepartmentId"] = new SelectList(_context.Department, "DepartmentId", "DepartmentName");
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Course.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Courses");
        }
    }
}