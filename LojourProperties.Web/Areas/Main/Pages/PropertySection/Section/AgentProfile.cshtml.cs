using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class AgentProfileModel : PageModel
    {
        private readonly UserManager<Profile> _userManager;

        public AgentProfileModel(UserManager<Profile> userManager)
        {
             _userManager = userManager;
        }

        public Profile Profile { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            Profile = await _userManager
                .Users
                .Include(x=>x.OperatingRegion)
                .FirstOrDefaultAsync(x=>x.Id == id);
            if(Profile == null)
            {
                return RedirectToPage("/Result", new { error = "Invalid Agent" });

            }
            return Page();
        }
    }
}
