﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.RegionPage
{
    [Authorize(Roles = "Admin,mSuperAdmin")]

    public class CreateModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public CreateModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["StateId"] = new SelectList(_context.States.OrderBy(x => x.StateName), "StateName", "StateName");
            ViewData["CategoryId"] = new SelectList(_context.CategoryLocations.OrderBy(x => x.Name), "Id", "Name");
 
            return Page();
        }

        [BindProperty]
        public OperatingRegion OperatingRegion { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            _context.OperatingRegions.Add(OperatingRegion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
