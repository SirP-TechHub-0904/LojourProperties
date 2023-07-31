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
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.AgentsPage
{
    [Authorize(Roles = "mSuperAdmin,Admin")]

    public class UpdateModel : PageModel
    {
        private readonly Domain.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public UpdateModel(Domain.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            Profile = await _userManager.FindByIdAsync(id);
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
           var updateprofile = await _userManager.FindByIdAsync(Profile.Id);
            updateprofile.UserStatus = Profile.UserStatus;
            updateprofile.SurName = Profile.SurName;
            updateprofile.FirstName = Profile.FirstName;
            updateprofile.LastName = Profile.LastName;
            updateprofile.Gender = Profile.Gender;
            updateprofile.MaritalStatus = Profile.MaritalStatus;
            updateprofile.PhoneNumber = Profile.PhoneNumber;
            updateprofile.AltPhone = Profile.AltPhone;
            updateprofile.StreetNumber = Profile.StreetNumber;
            updateprofile.StreetName = Profile.StreetName;
            updateprofile.City = Profile.City;
            updateprofile.State = Profile.State;
            updateprofile.ProfileType = Profile.ProfileType; 

            var result= await _userManager.UpdateAsync(updateprofile);
             
            if (result.Succeeded)
            {
                var regiosupdate = await _context.UserRegions.FirstOrDefaultAsync(x=>x.ProfileId == updateprofile.Id);
               regiosupdate.OperatingRegionId = Convert.ToInt64(Region);
               _context.Attach(regiosupdate).State = EntityState.Modified;


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
            TempData["error"] = "error";
            return Page();
        }

    }
}
