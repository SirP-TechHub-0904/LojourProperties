using Amazon.S3;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NirsalProject.Pages.Shared.ViewComponents
{
    public class HomePropertyViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
         
        public HomePropertyViewComponent(
            ApplicationDbContext context)
        {
            _context = context;
        }

 
        public async Task<IViewComponentResult> InvokeAsync()
        {
           var Property = await _context.Properties
               .Include(x => x.PrivacyCategory)
               .Include(x => x.PropertyCategory)
               .Include(x => x.PropertyType).ToListAsync();
             return View(Property);
        }
    }
}
