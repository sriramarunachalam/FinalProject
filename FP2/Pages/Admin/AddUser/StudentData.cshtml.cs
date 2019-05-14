using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FP2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles = "Admin")]
    public class StudentDataModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public StudentDataModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public string user { get; set; }
        public List<SelectListItem> Departments { get; set; }

        public IActionResult OnGet(string id)
        {
            user = id;
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");

            Departments = _context.Department.Select(a => new SelectListItem
            {
                Value=a.DepartmentId.ToString(),
                Text = a.DepartmentName
            }).ToList();

            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }       
        

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Student.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./StudentUsers");
        }
    }
}