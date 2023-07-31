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
using Microsoft.AspNetCore.Authorization;
using LojourProperties.Domain.Dtos;

namespace LojourProperties.UI.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class EditModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public EditModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Property Property { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Property = await _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyType).FirstOrDefaultAsync(m => m.Id == id);

            if (Property == null)
            {
                return NotFound();
            }
            ViewData["PrivacyCategoryId"] = new SelectList(_context.PrivacyCategories, "Id", "Name");

            ViewData["PropertyCategoryId"] = new SelectList(_context.PropertyCategories, "Id", "Name");

            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name");

            ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");


            ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "StateName", "StateName");


            var data = _context.Users.Include(x => x.UserRegions).Where(x => x.Email != "admin@lojour.com").OrderBy(x => x.SurName).AsQueryable();
            var output = data.Select(x => new AgentInfo
            {
                Id = x.Id,
                Name = x.Fullname
            });

            ViewData["AgentId"] = new SelectList(output, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Property.LastUpdated = DateTime.UtcNow.AddHours(1);
            _context.Attach(Property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(Property.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Details", new { id = Property.Id });
        }

        private bool PropertyExists(long id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
