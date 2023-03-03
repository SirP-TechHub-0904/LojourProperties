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
    public class SliderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;



        public SliderViewComponent(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public string UserInfo { get; set; }

        public async Task<IViewComponentResult> InvokeAsync(string key)
        {

            var slider = await _context.Sliders.Where(x => x.Show == true).ToListAsync();
            return View(slider);
        }
    }
}
