﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using NuGet.Packaging;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class IndexModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public IndexModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Property> Property { get; set; }
        public IList<PropertyDtoList> PropertyDtoLists { get; set; }
        public async Task OnGetAsync()
        {


            IQueryable<Property> property = from s in _context.Properties
                .Include(x => x.Agent)
                .Include(x => x.CityLocation)
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                                            select s;
            Property = await property.ToListAsync();
            List<PropertyDtoList> categoriesList = new List<PropertyDtoList>();
            List<PropertyDtoList> activitiesList = new List<PropertyDtoList>();
            List<PropertyDtoList> propertyTypesList = new List<PropertyDtoList>();
            List<PropertyDtoList> propertyLink = new List<PropertyDtoList>();
            List<PropertyDtoList> Distress = new List<PropertyDtoList>();


            List<PropertyDtoList> iList = new List<PropertyDtoList>();
            var category = await _context.PropertyCategories.ToListAsync();
            foreach (var item in category)
            {
                categoriesList.Add(new PropertyDtoList
                {
                    Title = item.Name, // Assuming you have a "Title" property in the PropertyCategory class
                    Count = property.Where(x => x.PropertyCategoryId == item.Id).Count(), // You can set the count later based on your logic
                    Query = "PropertyCategory"
                });
            }

            var activity = await _context.PrivacyCategories.ToListAsync();
            foreach (var item in activity)
            {
                activitiesList.Add(new PropertyDtoList
                {
                    Title = item.Name, // Assuming you have a "Title" property in the PropertyCategory class
                    Count = property.Where(x => x.PrivacyCategoryId == item.Id).Count(), // You can set the count later based on your logic
                    Query = "PrivacyCategory"
                });
            }
            
            var propertydistress = property.Where(x=>x.Distress == true).AsQueryable();
            
                Distress.Add(new PropertyDtoList
                {
                    Title = "Distress", // Assuming you have a "Title" property in the PropertyCategory class
                    Count = propertydistress.Count(), // You can set the count later based on your logic
                    Query = "Distress"
                });
            

            var linkrank = property
   .GroupBy(p => p.PropertyLink)  // Group properties by PropertyLink
   .Select(group => new
   {
       PropertyLink = group.Key,
       Count = group.Count()
   })
   .ToList();
            foreach (var item in linkrank)
            {
                propertyLink.Add(new PropertyDtoList
                {
                    Title = item.PropertyLink.ToString(), // Assuming you have a "Title" property in the PropertyCategory class
                    Count = item.Count, // You can set the count later based on your logic
                    Query = "PropertyLink"
                });
            }

                     var xpropertyTypesList = property
   .GroupBy(p => p.Title)  // Group properties by PropertyLink
   .Select(group => new
   {
       Title = group.Key,
       Count = group.Count()
   })
   .ToList();
            foreach (var item in xpropertyTypesList)
            {
                propertyTypesList.Add(new PropertyDtoList
                {
                    Title = item.Title.ToString(), // Assuming you have a "Title" property in the PropertyCategory class
                    Count = item.Count, // You can set the count later based on your logic
                    Query = "Title"
                });
            }

            iList.AddRange(categoriesList);
            iList.AddRange(activitiesList);
            iList.AddRange(propertyTypesList);
            iList.AddRange(propertyLink);
            iList.AddRange(Distress);

            PropertyDtoLists = iList.ToList();
        }
    }

    public class PropertyDtoList
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public string Query { get; set; }
    }
}
