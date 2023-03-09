using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    public class UpdateAgencyModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateAgencyModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]

        public Property Property { get; set; }
        [BindProperty]
        public PropertyAgency Agency { get; set; }
        [BindProperty]

        public IList<PropertyAgency> PropertyAgency { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PropertyAgency = await _context.PropertyAgencies
                .Include(p => p.Property)
                .Include(p => p.Agency)
                .Where(x => x.PropertyId == id).ToListAsync();

            Property = await _context.Properties.FindAsync(id);
            if (Property == null)
            {
                return NotFound();
            }

            var alldoccategory = await _context.Agencies.ToListAsync();
            var addedcategory = _context.PropertyAgencies.Where(x => x.PropertyId == id).Select(x => x.AgencyId).ToList();

            var yettoadd = alldoccategory.Where(x => !addedcategory.Contains(x.Id)).ToList();

            ViewData["AgencyId"] = new SelectList(yettoadd, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            try
            {
                // Process file
                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);

                var fileExt = Path.GetExtension(file.FileName);
                var docName = $"{Guid.NewGuid()}{fileExt}";
                // call server

                var s3Obj = new Domain.Dtos.AwsDtos.S3Object()
                {
                    BucketName = "lojourxyz",
                    InputStream = memoryStream,
                    Name = docName
                };

                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.UploadFileReturnUrlAsync(s3Obj, cred, "");
                // 
                if (result.Message.Contains("200"))
                {
                    Agency.Url = result.Url;
                    Agency.Key = result.Key;
                }
                else
                {
                    TempData["error"] = "unable to upload image";
                    //return Page();
                }
            }
            catch (Exception c)
            {

            }

            Agency.Date = DateTime.UtcNow.AddHours(1);
            _context.PropertyAgencies.Add(Agency);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./UpdateAgency", new {id = Agency.PropertyId});
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
            if(property == null)
            {
                return RedirectToPage("/Result", new { error = "Property Not Found" });
            }
            Agency = await _context.PropertyAgencies.FindAsync(id);

            if (Agency != null)
            {
                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.DeleteObjectAsync(cred, "lojourxyz", Agency.Key);
                _context.PropertyAgencies.Remove(Agency);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted";
                return RedirectToPage("./UpdateAgency", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }



    }

}
