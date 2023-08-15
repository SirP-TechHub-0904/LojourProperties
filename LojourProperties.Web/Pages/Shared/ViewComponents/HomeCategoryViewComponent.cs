using Amazon.S3;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Dtos;
using LojourProperties.Domain.Dtos.AwsDtos;
using LojourProperties.Domain.Models;
using LojourProperties.Domain.Services.AWS;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NirsalProject.Pages.Shared.ViewComponents
{
    public class HomeCategoryViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public HomeCategoryViewComponent(
            ApplicationDbContext context)
        {
            _context = context;
        }


         public async Task<IViewComponentResult> InvokeAsync()
        {
            var Property = _context.OperatingRegions
                .Include(x => x.Categories).ThenInclude(x=>x.Locations)
                .AsQueryable();

            
           var OperatingRegions = await Property.ToListAsync();

            return View(OperatingRegions);
        }
    }
}
