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
using Microsoft.Extensions.DependencyInjection;

namespace FP2.Pages.Admin.AppRoles
{
    [Authorize(Roles ="Admin")]
    public class AddUserModel : PageModel
    {
        private readonly FP2Context _context;
        public AddUserModel(IServiceProvider serviceProvider, FP2Context context)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        IServiceProvider _serviceProvider;

        [BindProperty]
        public List<string> RoleId { get; set; } // bound to check boxes
                                                 // note that all check boxes are given the same name
                                                 // so that they return an array of those check boxes that are checked
                                                 // <input type="checkbox" name="RoleId" value="@item.RoleId" />
                                                 // @item.RoleId is obtained from RolesList
        public List<RoleInfo> RolesList { get; set; } // will be bound to checkboxes

        [BindProperty]
        public UserInfo UInfo { get; set; } // will be bound to email, password

        public async Task<IActionResult> OnGetAsync()
        {
            UInfo = new UserInfo();
            RolesList = await _context.AspNetRoles.Select(m => new RoleInfo
            {
                RoleId = m.Id,
                RoleName = m.Name
            }).ToListAsync<RoleInfo>();

            return Page();        
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            //add role to the database received in BindProperty
            var user = new IdentityUser { UserName = UInfo.Email, Email = UInfo.Email };
            var result = await UserManager.CreateAsync(user, UInfo.Password);
            if (result.Succeeded)
            {
                // add roles for user
                foreach (string roleid in RoleId)
                {
                    var role = _context.AspNetRoles.FirstOrDefault<AspNetRoles>(m => roleid == m.Id);
                    await UserManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToPage("./ManageUsers");
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }
    }
}