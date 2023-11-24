using LojourProperties.Domain.Dtos;
using LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
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
        public string Title { get; set; }
        public string Query { get; set; }
        public bool Fine { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 21; // Number of items to display per page

        public IList<HomePropertyDto> PropertyDtoLists { get; set; }
        public async Task OnGetAsync(int? pg, string category, long? region, bool distress, string searchString, string find, string? location)
        {

            if (String.IsNullOrEmpty(searchString))
            {
                if (category != null) { Title = category; };
                if (region != null)
                {
                    var cat = await _context.CategoryLocations.FirstOrDefaultAsync(x => x.Id == region);
                    if (cat != null)
                    {
                        Title = Title + " " + cat.Name;
                    }

                }
                if (distress == true)
                {
                    Title = Title + " Distress";
                }
                Title = Title + " Properties";
            }
            else
            {
                Query = searchString;
                Title = "Search Result for " + searchString;
            }
            CurrentPage = pg ?? 1;
            var Property = _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.CityLocation)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyFeatures)
                .ThenInclude(x => x.FeaturesCategory)
                .Include(x => x.PropertyDocuments)
                .Include(x => x.PropertyAgencies)
                .Where(x => x.PropertyStatus == Domain.Models.Enum.PropertyStatus.Publish).OrderBy(x => x.SortOrder)
                .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                string amountPattern = @"[\d,.]+";
                Property = Property.Where(x => x.PropertyCategory.Name.Contains(searchString) 
                || x.Title.Contains(searchString)
                ||x.CityLocation.Name.Contains(searchString)
                || x.FullDescription.Contains(searchString)
                || x.ShortDescription.Contains(searchString)
                
                ).AsQueryable();

            }

            var outcome = Property.Select(x => new HomePropertyDto
            {
                Id = x.Id,
                Key = x.Key,
                Title = x.Title,
                SortOrder = x.SortOrder,
                Amount = string.Format("₦{0:N0}", x.Amount),

                Privacy = x.PrivacyCategory.Name,
                Category = x.PropertyCategory.Name,
                Activity = x.ActivityStatus,
                Image = x.PropertyImages.FirstOrDefault(img => img.Default == true).ImageUrl ?? "default-image-url",
                PropertyAgencies = x.PropertyAgencies,
                PropertyDocuments = x.PropertyDocuments,
                PropertyFeatures = x.PropertyFeatures,
                Description = x.ShortDescription,
                LojourID = x.PropertyRefID



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
