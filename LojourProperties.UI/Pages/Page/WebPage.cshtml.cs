using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.UI.Pages.Page
{
    public class WebPageModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public WebPageModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public WebPage WebPage { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return RedirectToPage("/Result", new { error = "Invalid Page" });
            }

            WebPage = await _context.WebPages.FirstOrDefaultAsync(m => m.Key == id);

            if (WebPage == null)
            {
                return RedirectToPage("/Result", new { error = "Invalid Key" });
            }
            if (WebPage.Publish == false)
            {
                return RedirectToPage("/Result", new { error = "Ristricted Page" });
            }
            return Page();
        }
    }
}
