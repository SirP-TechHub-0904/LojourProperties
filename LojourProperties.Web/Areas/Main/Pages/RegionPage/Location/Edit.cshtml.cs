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

namespace LojourProperties.Web.Areas.Main.Pages.RegionPage.Location
{
    public class EditModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public EditModel(LojourProperties.Domain.Data.ApplicationDbContext context)
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
           ViewData["CategoryLocationId"] = new SelectList(_context.CategoryLocations, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           

            _context.Attach(CityLocation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityLocationExists(CityLocation.Id))
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

        private bool CityLocationExists(long id)
        {
            return _context.CityLocations.Any(e => e.Id == id);
        }
    }
}
