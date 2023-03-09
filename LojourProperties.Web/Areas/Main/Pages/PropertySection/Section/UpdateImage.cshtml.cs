using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class UpdateImageModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateImageModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]

        public Property Property { get; set; }
        [BindProperty]
        public PropertyImage Image { get; set; }
        [BindProperty]

        public IList<PropertyImage> PropertyImage { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PropertyImage = await _context.PropertyImages
                .Include(p => p.Property).Where(x => x.PropertyId == id).ToListAsync();

            Property = await _context.Properties.FindAsync(id);
            if (Property == null)
            {
                return NotFound();
            }
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
                    Image.ImageUrl = result.Url;
                    Image.Key = result.Key;
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

            Image.Date = DateTime.UtcNow.AddHours(1);
            _context.PropertyImages.Add(Image);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./UpdateImage", new {id = Image.PropertyId});
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
            Image = await _context.PropertyImages.FindAsync(id);

            if (Image != null)
            {
                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.DeleteObjectAsync(cred, "lojourxyz", Image.Key);
                _context.PropertyImages.Remove(Image);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted";
                return RedirectToPage("./UpdateImage", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }

        public async Task<IActionResult> OnPostDefault(long? id, long? pid)
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
            Image = await _context.PropertyImages.FindAsync(id);

            if (Image != null)
            {
                var allimages = _context.PropertyImages.Where(x => x.PropertyId == pid).AsQueryable();
                foreach(var x in allimages)
                {
                    x.Default = false;
                    _context.Attach(x).State = EntityState.Modified;
                }
                Image.Default = true;
                _context.Attach(Image).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                TempData["success"] = "Default Updated";
                return RedirectToPage("./UpdateImage", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }


    }

}
