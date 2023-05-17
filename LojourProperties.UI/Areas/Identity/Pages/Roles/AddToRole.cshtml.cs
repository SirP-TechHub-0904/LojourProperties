using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojourProperties.UI.Areas.Identity.Pages.Roles
{
    public class AddToRoleModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<Profile> _userManager;


        public AddToRoleModel(LojourProperties.Domain.Data.ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<Profile> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Profile Profile { get; set; }
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ViewData["RolesId"] = new SelectList(_roleManager.Roles, "Id", "Name");
            Profile = await _userManager.FindByIdAsync(id);
            if(Profile == null)
            {
                TempData["error"] = "Unable to fetch user.";
                return RedirectToPage("./UserRoles");
            }
            return Page();
        }

        [BindProperty]
        public Slider Slider { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sliders.Add(Slider);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }

}
