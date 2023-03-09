using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    public class UpdateDocumentModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        public UpdateDocumentModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]

        public Property Property { get; set; }
        [BindProperty]
        public PropertyDocument Doc { get; set; }
        [BindProperty]

        public IList<PropertyDocument> PropertyDocument { get; set; }

        [BindProperty]
        public IFormFile file { get; set; }
        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PropertyDocument = await _context.PropertyDocuments
                .Include(p => p.Property)
                .Include(p => p.DocumentCategory)
                .Where(x => x.PropertyId == id).ToListAsync();

            Property = await _context.Properties.FindAsync(id);
            if (Property == null)
            {
                return NotFound();
            }

            var alldoccategory = await _context.DocumentCategories.ToListAsync();
            var addedcategory = _context.PropertyDocuments.Where(x => x.PropertyId == id).Select(x => x.DocumentCategoryId).ToList();

            var yettoadd = alldoccategory.Where(x => !addedcategory.Contains(x.Id)).ToList();

            ViewData["DocumentCategoryId"] = new SelectList(yettoadd, "Id", "Name");

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
                    Doc.Url = result.Url;
                    Doc.Key = result.Key;
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

            Doc.Date = DateTime.UtcNow.AddHours(1);
            _context.PropertyDocuments.Add(Doc);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./UpdateDocument", new {id = Doc.PropertyId});
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
            Doc = await _context.PropertyDocuments.FindAsync(id);

            if (Doc != null)
            {
                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.DeleteObjectAsync(cred, "lojourxyz", Doc.Key);
                _context.PropertyDocuments.Remove(Doc);
                await _context.SaveChangesAsync();
                TempData["success"] = "Deleted";
                return RedirectToPage("./UpdateDocument", new { id = pid });
            }
            return RedirectToPage("/Result", new { error = "image Not Found" });

        }



    }

}
