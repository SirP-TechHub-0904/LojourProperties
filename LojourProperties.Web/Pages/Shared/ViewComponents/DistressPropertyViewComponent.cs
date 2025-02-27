﻿using Amazon.S3;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Dtos;
using LojourProperties.Domain.Dtos.AwsDtos;
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
    public class DistressPropertyViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public DistressPropertyViewComponent(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Property = _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyFeatures)
                .ThenInclude(x=>x.FeaturesCategory)
                .Include(x => x.PropertyDocuments)
                .Include(x => x.PropertyAgencies) .Where(x=>x.Distress == true && x.PropertyStatus == LojourProperties.Domain.Models.Enum.PropertyStatus.Publish).OrderBy(x=>x.SortOrder).Take(6).AsQueryable();

            var outcome = Property.Select(x => new HomePropertyDto
            {
                Id = x.Id,
                Key = x.Key,
                Title = x.Title,
                SortOrder = x.SortOrder,
                Amount = string.Format("₦{0:N0}", x.Amount), 
                  Privacy = x.PrivacyCategory.Name,
                Category = x.PropertyCategory.Name,
                Activity = x.ActivityStatus,
                Image = x.PropertyImages.FirstOrDefault(img => img.Default == true).ImageUrl ?? "default-image-url",
                PropertyAgencies = x.PropertyAgencies,
                PropertyDocuments = x.PropertyDocuments,
                PropertyFeatures = x.PropertyFeatures,
                Description = x.ShortDescription,
                LojourID = x.PropertyRefID
         
            
            
            });



            return View(outcome.Take(6).ToList());
        }
    }
}
