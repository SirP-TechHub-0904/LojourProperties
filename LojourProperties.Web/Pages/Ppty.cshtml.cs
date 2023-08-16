using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage;
using LojourProperties.Domain.Dtos;

namespace LojourProperties.Web.Pages
{
    public class PptyModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public PptyModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Property Property { get; set; }
        public IList<HomePropertyDto> RelatedProperty { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id, string ppy)
        {
            if (id == null)
            {
                return NotFound();
            }

            Property = await _context.Properties
                .Include(x => x.Agent)
                .Include(x => x.CityLocation)
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyVideos)
                .Include(x => x.PropertyFeatures)
                .ThenInclude(x => x.FeaturesCategory)
                .Include(x => x.PropertyType).FirstOrDefaultAsync(m => m.Id == id && m.PropertyStatus == Domain.Models.Enum.PropertyStatus.Publish);

            if (Property == null)
            {
                return NotFound();
            }
            var Propertylist = _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyFeatures)
                .ThenInclude(x => x.FeaturesCategory)
                .Include(x => x.PropertyDocuments)
                .Include(x => x.PropertyAgencies)
                .Include(x => x.PropertyType)
                .Where(x => x.PropertyStatus == Domain.Models.Enum.PropertyStatus.Publish && x.PropertyCategory.Id == Property.PropertyCategoryId).OrderBy(x => x.SortOrder)
                .Take(5).AsQueryable();

              var outcome = Propertylist.Select(x => new HomePropertyDto
            {
                Id = x.Id,
                Key = x.Key,
                Title = x.Title,
                SortOrder = x.SortOrder,
                Amount = string.Format("₦{0:N0}", x.Amount),
                State = x.State,
                Country = x.Country,
                Privacy = x.PrivacyCategory.Name,
                Category = x.PropertyCategory.Name,
                Activity = x.ActivityStatus,
                Image = x.PropertyImages.FirstOrDefault(img => img.Default == true).ImageUrl ?? "default-image-url",
                PropertyAgencies = x.PropertyAgencies,
                PropertyDocuments = x.PropertyDocuments,
                PropertyFeatures = x.PropertyFeatures,
                  Description = x.ShortDescription



            });
            RelatedProperty = await outcome.ToListAsync();
            return Page();
        }
    }
}
