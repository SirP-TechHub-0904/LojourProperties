using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class UpdateFeaturesModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateFeaturesModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]
        public Property Property { get; set; }


        [BindProperty]
        public PropertyFeature Feature { get; set; }
        [BindProperty]

        public IList<PropertyFeature> PropertyFeature { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PropertyFeature = await _context.PropertyFeatures
                .Include(p => p.Property)
                .Include(p => p.FeaturesCategory)

                .Where(x => x.PropertyId == id).ToListAsync();

            Property = await _context.Properties.FindAsync(id);
            if (Property == null)
            {
                return NotFound();
            }
            var allfeatures = await _context.FeaturesCategories.ToListAsync();
            var addedfeature = _context.PropertyFeatures.Where(x => x.PropertyId == id).Select(x => x.FeaturesCategoryId).ToList();

            var yettoadd = allfeatures.Where(x=> !addedfeature.Contains(x.Id)).ToList();
            ViewData["FeaturesCategoryId"] = new SelectList(yettoadd, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
             
            _context.PropertyFeatures.Add(Feature);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./UpdateFeatures", new {id = Feature.PropertyId});
        }

        public async Task<IActionResult> OnPostDelete(long? id, long? pid)
        {
            if (id == null)
            {
                return RedirectToPage("/Result", new { error = "Invalid Data" });
            }
            if (pid == null)
            {
                return RedirectToPage("/Result", new { error = "Invalid Data" });
            }
            var property = await _context.Properties.FindAsync(pid);
            if (property == null)
            {
                return RedirectToPage("/Result", new { error = "Property Not Found" });
            }
            Feature = await _context.PropertyFeatures.FindAsync(id);

            if (Feature != null)
            {
               
                _context.PropertyFeatures.Remove(Feature);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted";
                return RedirectToPage("./UpdateFeatures", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }
    }

}
