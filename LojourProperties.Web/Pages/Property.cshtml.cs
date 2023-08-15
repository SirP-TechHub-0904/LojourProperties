using LojourProperties.Domain.Dtos;
using LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Xml.Linq;

namespace LojourProperties.Web.Pages
{
    public class PropertyModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public PropertyModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public List<HomePropertyDto> PaginatedData { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20; // Number of items to display per page

        public IList<HomePropertyDto> PropertyDtoLists { get; set; }
        public async Task OnGetAsync(int? pg, string category, long? region)
        {
            CurrentPage = pg ?? 1;
            var Property = _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyFeatures)
                .ThenInclude(x => x.FeaturesCategory)
                .Include(x => x.PropertyDocuments)
                .Include(x => x.PropertyAgencies)
                .Include(x => x.PropertyType)

                .AsQueryable();
            if(region != null)
            {
                Property = Property.Where(x => x.CityLocationId == region).AsQueryable();
            }
            if (!String.IsNullOrEmpty(category))
            {
                Property = Property.Where(x => x.PropertyCategory.Name.Contains(category)).AsQueryable();
            }
            var outcome = Property.Select(x => new HomePropertyDto
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
            PropertyDtoLists = await outcome.ToListAsync();



            // Get the total count of data
            int totalCount = PropertyDtoLists.Count();

            // Calculate the total number of pages
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            // Ensure the current page number is within the valid range
            if (CurrentPage < 1)
                CurrentPage = 1;
            else if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;

            // Get the paginated data from the data source
            int skipCount = (CurrentPage - 1) * PageSize;

            // Retrieve the data for the current page using Skip and Take LINQ methods
            List<HomePropertyDto> paginatedData = PropertyDtoLists.Skip(skipCount).Take(PageSize).ToList();
            PaginatedData = paginatedData;
        }
    }
}
