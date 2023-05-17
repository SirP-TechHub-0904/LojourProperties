using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojourProperties.Domain.Dtos;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.UI.Areas.Identity.Pages.Roles
{
    // [Authorize(Roles = "mSuperAdmin")]

    public class UserRolesModel : PageModel
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Profile> _userManager;

        public UserRolesModel(RoleManager<IdentityRole> roleManager, UserManager<Profile> userManager)
        {

            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<UsersViewModel> UsersWithRole = new List<UsersViewModel>();
        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            var users = _userManager.Users.Select(c => new UsersViewModel()
            {
                Fullname = c.Fullname,
                Email = c.Email,
                Role = string.Join(",", _userManager.GetRolesAsync(c).Result.ToArray())
            }).Where(x=> !String.IsNullOrEmpty(x.Role)).ToList();
            UsersWithRole = users.ToList();
            return Page();
        }
    }
}