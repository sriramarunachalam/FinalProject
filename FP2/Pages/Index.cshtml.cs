using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FP2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly FP2.Models.FP2Context _context;

        public IndexModel(UserManager<IdentityUser> userManager, FP2Context context)
        {
            _userManager = userManager;
            _context = context;
        }

        public string userid { get; set; }
        public Student Student { get; set; }
        public Professor Professor { get; set; }

        public void OnGet(long id)
        {
            userid = _userManager.GetUserId(HttpContext.User);
            Student = _context.Student.FirstOrDefault(s => s.UserId == userid);
            Professor = _context.Professor.FirstOrDefault(p => p.UserId == userid);
        }
    }
}
