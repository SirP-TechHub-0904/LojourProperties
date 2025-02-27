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

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DetailsModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Property Property { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Property = await _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory) 
                .Include(x => x.PropertyDocuments)
                .ThenInclude(x=>x.DocumentCategory)
                .Include(x => x.PropertyImages)
                .Include(x => x.PropertyVideos)
                .Include(x => x.PropertyAgencies)
                .ThenInclude(x=>x.Agency)
                .Include(x=>x.Agent)
                .Include(x=>x.PropertyFeatures).ThenInclude(x=>x.FeaturesCategory)
           .FirstOrDefaultAsync(m => m.Id == id);

            if (Property == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
