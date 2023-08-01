using Amazon.S3;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NirsalProject.Pages.Shared.ViewComponents
{
    public class HomeSearchViewComponent : ViewComponent
    {
        private readonly UserManager<LojourProperties.Domain.Models.Profile> _userManager;
        //private readonly IAmazonS3 _s3Client;
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IConfiguration _config;


        public HomeSearchViewComponent(
            UserManager<LojourProperties.Domain.Models.Profile> userManager, ApplicationDbContext context/*, IAmazonS3 s3Client*/, IStorageService storageService, IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            //_s3Client = s3Client;
            _storageService = storageService;
            _config = config;
        }

        public string UserInfo { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string key)
        {

          
              ViewData["OperatingRegionId"] = new SelectList(_context.OperatingRegions, "Id", "RegionOfOperation");
             
            return View();
        }
    }
}
