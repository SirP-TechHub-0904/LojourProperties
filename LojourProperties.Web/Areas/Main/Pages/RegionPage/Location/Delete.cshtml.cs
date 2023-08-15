using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.RegionPage.Location
{
    public class DeleteModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DeleteModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CityLocation CityLocation { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CityLocation = await _context.CityLocations
                .Include(c => c.CategoryLocation).FirstOrDefaultAsync(m => m.Id == id);

            if (CityLocation == null)
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

            CityLocation = await _context.CityLocations.FindAsync(id);

            if (CityLocation != null)
            {
                _context.CityLocations.Remove(CityLocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
