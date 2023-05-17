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

        public IList<Property> Property { get; set; }

        public async Task OnGetAsync()
        {
            Property = await _context.Properties
                .Include(x => x.PrivacyCategory)
                .Include(x => x.PropertyCategory)
                .Include(x => x.PropertyType).ToListAsync();
         }
    }
}