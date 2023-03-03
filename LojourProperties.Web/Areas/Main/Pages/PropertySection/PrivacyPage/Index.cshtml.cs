using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LojourProperties.Domain.Data;
using LojourProperties.Domain.Models;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.PrivacyPage
{
    public class IndexModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public IndexModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PrivacyCategory> PrivacyCategory { get;set; }

        public async Task OnGetAsync()
        {
            PrivacyCategory = await _context.PrivacyCategories.ToListAsync();
        }
    }
}
