using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Features
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class CreateModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public CreateModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FeaturesCategoryId"] = new SelectList(_context.FeaturesCategories, "Id", "Id");
        ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "AreaGuide");
            return Page();
        }

        [BindProperty]
        public PropertyFeature PropertyFeature { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PropertyFeatures.Add(PropertyFeature);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
