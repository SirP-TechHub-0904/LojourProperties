using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using LojourProperties.Domain.Services.ImageResize;
using LojourProperties.Domain.Dtos.AwsDtos;

namespace LojourProperties.Web.Areas.Main.Pages.RegionPage.Category
{
    public class CreateModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;
        private readonly IFileImageResize _fileImageResize;
        public CreateModel(LojourProperties.Domain.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment, IConfiguration config, IStorageService storageService, IFileImageResize fileImageResize)
        {
            _context = context;
            _environment = environment;
            _config = config;
            _storageService = storageService;
            _fileImageResize = fileImageResize;
        }
         
        [BindProperty]
        public IFormFile? file { get; set; }


        public IActionResult OnGet()
        {
        ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "State");
            return Page();
        }

        [BindProperty]
        public CategoryLocation CategoryLocation { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
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
                    CategoryLocation.ImageUrl = result.Url;
                    CategoryLocation.ImageKey = result.Key;
 
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


            _context.CategoryLocations.Add(CategoryLocation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
