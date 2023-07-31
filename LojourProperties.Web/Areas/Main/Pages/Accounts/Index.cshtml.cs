using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.Accounts
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class IndexModel : PageModel
    {
        private readonly Domain.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public IndexModel(Domain.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Profile> Profile { get; set; }
        public int Agents { get; set; }
        public int Diamond { get; set; }
        public int Silver { get; set; }
        public int Owner { get; set; }
        public int AllUsers { get; set; }

        public async Task OnGetAsync()
        {

            IQueryable<Profile> profilex = from s in _context.Users
                                           .Include(x=>x.UserRegions)
                                           .Where(x => x.Email != "admin@lojour.com")
                                           select s;
            Profile = await profilex.ToListAsync();

            AllUsers = profilex.Count();
            Agents = profilex.Where(x=>x.ProfileType == Domain.Models.Enum.ProfileType.Agent).Count();
            Owner = profilex.Where(x=>x.ProfileType == Domain.Models.Enum.ProfileType.LandLord).Count();
           
        }



    }

}
