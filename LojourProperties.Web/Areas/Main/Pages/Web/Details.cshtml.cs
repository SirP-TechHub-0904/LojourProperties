using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.Web
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class DetailsModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DetailsModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public WebPage WebPage { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebPage = await _context.WebPages.FirstOrDefaultAsync(m => m.Id == id);

            if (WebPage == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
