using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Features
{
    public class EditModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public EditModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PropertyFeature PropertyFeature { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PropertyFeature = await _context.PropertyFeatures
                .Include(p => p.FeaturesCategory)
                .Include(p => p.Property).FirstOrDefaultAsync(m => m.Id == id);

            if (PropertyFeature == null)
            {
                return NotFound();
            }
           ViewData["FeaturesCategoryId"] = new SelectList(_context.FeaturesCategories, "Id", "Id");
           ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "AreaGuide");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PropertyFeature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyFeatureExists(PropertyFeature.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PropertyFeatureExists(long id)
        {
            return _context.PropertyFeatures.Any(e => e.Id == id);
        }
    }
}
