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

namespace LojourProperties.UI.Areas.Main.Pages.Others.Images
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DetailsModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public PropertyImage PropertyImage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PropertyImage = await _context.PropertyImages
                .Include(p => p.Property).FirstOrDefaultAsync(m => m.Id == id);

            if (PropertyImage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
