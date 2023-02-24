using Amazon.S3;
using Amazon.S3.Model;
using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojourProperties.Web.Areas.Main.Pages.FileManager
{
    public class IndexModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;

        public IndexModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
        }

        public async Task<IActionResult> OnGet()
        {

            return Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        [BindProperty]
        public IFormFile file { get; set; }
        public async Task OnPostAsync()
        {

            // Process file
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var fileExt = Path.GetExtension(file.FileName);
            var docName = $"{Guid.NewGuid()}{fileExt}";
            // call server

            var s3Obj = new Domain.Dtos.AwsDtos.S3Object()
            {
                BucketName = "code4humans0904",
                InputStream = memoryStream,
                Name = docName
            };

            var cred = new AwsCredentials()
            {
                AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                SecretKey = _config["AwsConfiguration:AWSSecretKey"]
            };

            var result = await _storageService.UploadFileAsync(s3Obj, cred);
            // 

        }


    }

}
