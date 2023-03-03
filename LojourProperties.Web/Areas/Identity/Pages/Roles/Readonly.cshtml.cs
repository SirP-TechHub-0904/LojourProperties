using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojourProperties.Web.Areas.Identity.Pages.Roles
{
  
        [AllowAnonymous]
        public class ReadonlyModel : PageModel
        {
            private readonly SignInManager<Profile> _signInManager;
            private readonly RoleManager<IdentityRole> _role;
            private readonly UserManager<Profile> _userManager;
            private readonly ILogger<ReadonlyModel> _logger;
            private readonly ApplicationDbContext _context;


            public ReadonlyModel(
                UserManager<Profile> userManager,
                SignInManager<Profile> signInManager,
                RoleManager<IdentityRole> role,
            ILogger<ReadonlyModel> logger,
                ApplicationDbContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _role = role;
                _context = context;
            }
            [BindProperty]
            public string RefId { get; set; }
            [BindProperty]
            public InputModel Input { get; set; }

            public string ReturnUrl { get; set; }

            public IList<AuthenticationScheme> ExternalLogins { get; set; }

            public class InputModel
            {
                [Required]
                [EmailAddress]
                [Display(Name = "Email")]
                public string Email { get; set; }

                [Required]
                [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
                [DataType(DataType.Password)]
                [Display(Name = "Password")]
                public string Password { get; set; }

                [DataType(DataType.Password)]
                [Display(Name = "Confirm password")]
                [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
                public string ConfirmPassword { get; set; }
                public string Phone { get; set; }

                [Required]
                [Display(Name = "Full Name")]
                public string FullName { get; set; }
                [Display(Name = "Referral ID")]

                public string UserReferral { get; set; }
                [Display(Name = "Wallet Address")]
                public string WalletAddress { get; set; }

                public string SecurityQuestion { get; set; }
                public string SecurityAnswer { get; set; }
                public string Referal { get; set; }
            }

            // public string REFID { get; set; }
            public async Task OnGetAsync(string returnUrl = null, string refxid = null)
            {

                //if (ModelState.IsValid)
                //{
                    var user = new Profile
                    {
                        UserName = "admin@lojour.com",
                        Email = "admin@lojour.com",
                        PhoneNumber = "000",
                        FirstName = "Admin",
                        Title = "MR",
                        SurName = "Admin",
                        LastName = "Admin",
                        Gender = "Male",
                        MaritalStatus = "Single",
                        Description = "Admin",
                        StreetNumber ="0",
                        StreetName="ABJ",
                        ZipCode="ABJ",
                        City="ABJ",
                        Country ="ABJ",
                        State="ABJ",
                        LojourId="L001",
                        EmailConfirmed = true,
                        DateRegistered = DateTime.UtcNow.AddHours(1),
                        DateOfBirth = DateTime.UtcNow.AddHours(1)

                    };
                    


                    user.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(user, "AdminIn2023@");
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");



                        await _userManager.AddToRoleAsync(user, "mSuperAdmin");
                        
                        //return LocalRedirect(returnUrl);

                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                //}


            }
        
    }
}
