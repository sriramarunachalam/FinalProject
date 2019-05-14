using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FP2.Models;

namespace FP2.Pages.Admin.AppRoles
{
    public class CreateModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public CreateModel(IServiceProvider serviceProvider, FP2.Models.FP2Context context)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        IServiceProvider _serviceProvider;

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AspNetRoles AspNetRoles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AspNetRoles.Add(AspNetRoles);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}