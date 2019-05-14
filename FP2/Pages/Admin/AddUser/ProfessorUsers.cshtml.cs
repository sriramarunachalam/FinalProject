using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles = "Admin")]
    public class ProfessorUsersModel : PageModel
    {
        private readonly FP2Context _context;

        public readonly UserManager<IdentityUser> _userManager;

        public IList<Professor> Professor { get; set; }

        public List<string> userids = new List<string>();

        public ProfessorUsersModel(FP2.Models.FP2Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<IdentityUser> ProfessorUsers { get; set; }

        public async Task OnGetAsync()
        {
            ProfessorUsers = await _userManager.GetUsersInRoleAsync("Professor");
            ProfessorUsers = ProfessorUsers.OrderBy(i => i.UserName).ToList();

            Professor = await _context.Professor.ToListAsync();

            foreach (var i in Professor)
            {
                userids.Add(i.UserId);
            }
        }
    }
}