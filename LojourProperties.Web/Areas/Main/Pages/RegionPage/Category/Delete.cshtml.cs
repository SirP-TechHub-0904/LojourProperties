using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.RegionPage.Category
{
    public class DeleteModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DeleteModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CategoryLocation CategoryLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryLocation = await _context.CategoryLocations
                .Include(c => c.OperatingRegion).FirstOrDefaultAsync(m => m.Id == id);

            if (CategoryLocation == null)
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

            CategoryLocation = await _context.CategoryLocations.FindAsync(id);

            if (CategoryLocation != null)
            {
                _context.CategoryLocations.Remove(CategoryLocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
