using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojourProperties.Web.Areas.Main.Pages.Slides
{
    public class CreateModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;

        public CreateModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }
        [BindProperty]
        public IFormFile file { get; set; }

        [BindProperty]
        public IFormFile smallfile { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Slider Slider { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            
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
                    Slider.MainImageUrl = result.Url;
                    Slider.MainImageKey = result.Key;
                }
                else
                {
                    TempData["error"] = "unable to upload image";
                    //return Page();
                }
            }catch(Exception c)
            {

            }

            try
            {
                // Process file
                await using var memoryStream = new MemoryStream();
                await smallfile.CopyToAsync(memoryStream);

                var fileExt = Path.GetExtension(smallfile.FileName);
                var docName = $"{Guid.NewGuid()}{fileExt}";
                // call server

                var xs3Obj = new Domain.Dtos.AwsDtos.S3Object()
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

                var xresult = await _storageService.UploadFileReturnUrlAsync(xs3Obj, cred, "");
                // 
                if (xresult.Message.Contains("200"))
                {
                    Slider.Url = xresult.Url;
                    Slider.Key = xresult.Key;
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
            Slider.Date = DateTime.UtcNow.AddHours(1);
            _context.Sliders.Add(Slider);
            await _context.SaveChangesAsync();
            TempData["success"] = "Created";
            return RedirectToPage("./Index");
        }
    }
}
