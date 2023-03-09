using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.Accounts
{
     
        [Authorize(Roles = "mSuperAdmin,Admin")]

        public class NewModel : PageModel
        {
            private readonly Domain.Data.ApplicationDbContext _context;
            private readonly UserManager<Profile> _userManager;

            public NewModel(Domain.Data.ApplicationDbContext context, UserManager<Profile> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

            public IActionResult OnGet()
            {

                ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "Id", "StateName");
                ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");

                return Page();
            }

            [BindProperty]
            public Profile Profile { get; set; }

            //[BindProperty]
            //public InputModel Input { get; set; }

            //public class InputModel
            //{
            //    [Required]
            //    [EmailAddress]
            //    [Display(Name = "Email")]
            //    public string Email { get; set; }

            //}

            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://aka.ms/RazorPagesCRUD.
            public async Task<IActionResult> OnPostAsync()
            {
                Profile.DateRegistered = DateTime.Now;
                Profile.LojourId = "XYZ";
                var result = await _userManager.CreateAsync(Profile, "XYZ123@20");
                if (result.Succeeded)
                {

                    return RedirectToPage("./Index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

        }
    }
