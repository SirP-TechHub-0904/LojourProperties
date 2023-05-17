using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace LojourProperties.UI.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class UpdateVideoModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateVideoModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]

        public Property Property { get; set; }
        [BindProperty]
        public PropertyVideo Video { get; set; }
        [BindProperty]

        public IList<PropertyVideo> PropertyVideo { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PropertyVideo = await _context.PropertyVideos
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
                    Video.VideoUrl = result.Url;
                    Video.Key = result.Key;
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

            Video.Date = DateTime.UtcNow.AddHours(1);
            _context.PropertyVideos.Add(Video);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./UpdateVideo", new {id = Video.PropertyId});
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
            Video = await _context.PropertyVideos.FindAsync(id);

            if (Video != null)
            {
                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.DeleteObjectAsync(cred, "lojourxyz", Video.Key);
                _context.PropertyVideos.Remove(Video);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted";
                return RedirectToPage("./UpdateVideo", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }
    }

}
