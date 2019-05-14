using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using FP2.Pages.Admin.AddUser;

namespace FP2.Pages.Admin.AppRoles
{
    public class ManageUsersModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public ManageUsersModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<UserVM> UserList { get;set; }

        public async Task OnGetAsync()
        {
            var res = await _context.AspNetUsers.ToListAsync();
            UserList = (from item in res
                        select new UserVM
                        {
                        UserName = item.UserName,
                        Email = item.Email,
                        Id = item.Id
                        }).ToList<UserVM>();
        }
    }
}
