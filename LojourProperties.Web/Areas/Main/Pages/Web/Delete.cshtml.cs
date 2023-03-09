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
    public class DeleteModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public DeleteModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WebPage = await _context.WebPages.FindAsync(id);

            if (WebPage != null)
            {
                _context.WebPages.Remove(WebPage);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
