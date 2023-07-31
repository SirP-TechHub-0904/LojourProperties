using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.AgentsPage
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class CreateModel : PageModel
    {
        private readonly Domain.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public CreateModel(Domain.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {

            ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");

            return Page();
        }

        [BindProperty]
        public Profile Profile { get; set; }

        [BindProperty]
        public string Region { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Profile.DateRegistered = DateTime.Now;
            Profile.LojourId = "LP";
            Profile.UserName = Profile.Email;
            Profile.UserStatus = Domain.Models.Enum.UserStatus.Active;


            var result = await _userManager.CreateAsync(Profile, "XYZ123@20");
            if (result.Succeeded)
            {
                UserRegion r = new UserRegion();
                r.ProfileId = Profile.Id;
                r.OperatingRegionId = Convert.ToInt64(Region);
                _context.UserRegions.Add(r);
                await _context.SaveChangesAsync();

                //var updateid = await _userManager.FindByIdAsync(Profile.Id);
                //updateid.LojourId = "LP"+ PRO

                TempData["success"] = "successfull";
                return RedirectToPage("./Index");

            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");

            return Page();
        }

    }
}
