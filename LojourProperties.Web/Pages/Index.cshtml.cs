using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Testimony> Testimonies { get;set;}
        public async Task OnGetAsync()
        {
            Testimonies = await _context.Testimonies.ToListAsync();
        }

        public async Task<IActionResult> OnPostSubmitForm(string email)
        {
            Newsletter n = new Newsletter();
            n.Email = email;
            n.Date = DateTime.Now;
                 _context.Newsletters.Add(n);
            await _context.SaveChangesAsync();
            TempData["ccsuccess"] = "Successful";
            return Page();
        }
    }
}