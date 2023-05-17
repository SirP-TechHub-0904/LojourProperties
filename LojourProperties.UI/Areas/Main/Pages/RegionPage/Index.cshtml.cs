using System;
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

namespace LojourProperties.UI.Areas.Main.Pages.RegionPage
{
    [Authorize(Roles = "Admin,mSuperAdmin")]

    public class IndexModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public IndexModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OperatingRegion> OperatingRegion { get;set; }

        public async Task OnGetAsync()
        {
            OperatingRegion = await _context.OperatingRegions.ToListAsync();
        }
    }
}
