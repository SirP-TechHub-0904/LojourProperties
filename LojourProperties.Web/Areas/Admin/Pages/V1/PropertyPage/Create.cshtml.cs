using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage
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
            ViewData["PrivacyCategoryId"] = new SelectList(_context.PrivacyCategories, "Id", "Name");

            ViewData["PropertyCategoryId"] = new SelectList(_context.PropertyCategories, "Id", "Name");

            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name");

            ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");


            ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewData["CityLocationId"] = new SelectList(_context.CityLocations.OrderBy(x => x.Name), "Id", "Name");


            var data = _context.Users.Include(x => x.UserRegions).Where(x => x.Email != "admin@lojour.com" && x.Email != "m@lojour.com").OrderBy(x => x.SurName).AsQueryable();
            var output = data.Select(x => new AgentInfo
            {
                Id = x.Id,
                Name = x.Fullname 
            });

            ViewData["AgentId"] = new SelectList(output, "Id", "Name");

            return Page();
        }

        [BindProperty]
        public Property Property { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Guid guid = Guid.NewGuid();
            Property.Key = guid.ToString();
            Property.Date = DateTime.UtcNow.AddHours(1);
            Property.DateAdded = DateTime.UtcNow.AddHours(1);
            Property.LastUpdated = DateTime.UtcNow.AddHours(1);
            Property.PropertyRefID = "X";
            Property.ActivityStatus = Domain.Models.Enum.ActivityStatus.Available;

            _context.Properties.Add(Property);
            await _context.SaveChangesAsync();
            ///
            var updateid = await _context.Properties.FindAsync(Property.Id);
            updateid.PropertyRefID = "LOJ" + updateid.Id.ToString("0000");
            _context.Attach(updateid).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Details", new { id = updateid.Id });
        }
    }
}
