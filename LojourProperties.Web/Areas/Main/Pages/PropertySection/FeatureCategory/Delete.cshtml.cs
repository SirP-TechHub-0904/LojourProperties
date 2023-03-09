using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.FeatureCategory
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class DeleteModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DeleteModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FeaturesCategory FeaturesCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeaturesCategory = await _context.FeaturesCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (FeaturesCategory == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FeaturesCategory = await _context.FeaturesCategories.FindAsync(id);

            if (FeaturesCategory != null)
            {
                _context.FeaturesCategories.Remove(FeaturesCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
