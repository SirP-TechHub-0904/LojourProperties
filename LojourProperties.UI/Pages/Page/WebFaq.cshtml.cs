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
    public class WebFaqModel : PageModel
    {
        private readonly LojourProperties.Domain.Data.ApplicationDbContext _context;

        public WebFaqModel(LojourProperties.Domain.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FAQ> FAQ { get;set; }
        public IList<SafetyTip> SafetyTip { get;set; }

        public async Task OnGetAsync()
        {
            FAQ = await _context.FAQs.Where(x=>x.Show == true).OrderBy(x=>x.SortOrder).ToListAsync();
            SafetyTip = await _context.SafetyTips.Where(x=>x.Show == true).OrderBy(x=>x.SortOrder).ToListAsync();
        }
    }
}
