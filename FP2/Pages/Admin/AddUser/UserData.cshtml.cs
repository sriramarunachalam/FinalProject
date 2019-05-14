using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles = "Admin")]
    public class UserDataModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public UserDataModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
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

            return RedirectToPage("./Users");
        }
    }
}