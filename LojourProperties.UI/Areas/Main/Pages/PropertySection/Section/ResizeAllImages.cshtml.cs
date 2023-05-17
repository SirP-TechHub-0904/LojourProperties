using LojourProperties.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LojourProperties.UI.Areas.Main.Pages.PropertySection.Section
{
    public class ResizeAllImagesModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public ResizeAllImagesModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PropertyImage> PropertyImage { get; set; }

        public async Task OnGetAsync()
        {
            PropertyImage = await _context.PropertyImages.ToListAsync();
            foreach(var x in PropertyImage)
            {

            }
        }
    }
}
