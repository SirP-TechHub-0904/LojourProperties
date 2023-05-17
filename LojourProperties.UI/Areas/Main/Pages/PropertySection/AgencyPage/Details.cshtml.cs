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

namespace LojourProperties.UI.Areas.Main.Pages.PropertySection.AgencyPage
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DetailsModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Agency Agency { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Agency = await _context.Agencies.FirstOrDefaultAsync(m => m.Id == id);

            if (Agency == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
