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
    public class HomeCountViewComponent : ViewComponent
    {
           private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;
        private readonly UserManager<Profile> _userManager;

        public HomeCountViewComponent(LojourProperties.Domain.Data.ApplicationDbContext context, UserManager<Profile> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Property = _context.Properties.AsQueryable();
            int propertycount = Property.Count();
            int closedealspropertycount = Property.Where(x=>x.ActivityStatus != LojourProperties.Domain.Models.Enum.ActivityStatus.Available).Count();


            var users = await _userManager.Users.CountAsync();
            var location = await _context.CityLocations.CountAsync();
          
            ViewBag.p = propertycount + 1000;
            ViewBag.a = closedealspropertycount + 72;
            ViewBag.u = users + 40;
            ViewBag.l = location + 12;

            return View();
        }
    }
}
