using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Services.AWS;

namespace LojourProperties.Web.Areas.Main.Pages.Slides
{
    public class DeleteModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IStorageService _storageService;

        public DeleteModel(LojourProperties.Domain.Data.ApplicationDbContext context, IConfiguration config, IStorageService storageService)
        {
            _context = context;
            _config = config;
            _storageService = storageService;
        }

        [BindProperty]
        public Slider Slider { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slider = await _context.Sliders.FirstOrDefaultAsync(m => m.Id == id);

            if (Slider == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slider = await _context.Sliders.FindAsync(id);

            if (Slider != null)
            {
                var cred = new AwsCredentials()
                {
                    AccessKey = _config["AwsConfiguration:AWSAccessKey"],
                    SecretKey = _config["AwsConfiguration:AWSSecretKey"]
                };

                var result = await _storageService.DeleteObjectAsync(cred, "lojourxyz", Slider.Key);
                _context.Sliders.Remove(Slider);
                await _context.SaveChangesAsync();
            }
            TempData["success"] = "Deleted";
            return RedirectToPage("./Index");
        }
    }
}
