using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FP2.Models;
using Microsoft.AspNetCore.Authorization;

namespace FP2.Pages.Admin.AddUser
{
    [Authorize(Roles ="Admin")]
    public class UsersModel : PageModel
    {
        private readonly FP2.Models.FP2Context _context;

        public UsersModel(FP2.Models.FP2Context context)
        {
            _context = context;
        }

        public IList<UserVM> UserList { get; set; }

        public async Task OnGetAsync()
        {
            var res = await _context.AspNetUsers.ToListAsync();
            UserList = (from item in res
                        select new UserVM
                        {
                            UserName = item.UserName,
                            Email = item.Email,
                            Id = item.Id
                        }).OrderBy(i=>i.UserName).ToList<UserVM>();
        }
    }
}
