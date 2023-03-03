using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.Admin
{
    [Authorize(Roles = "Admin,mSuperAdmin")]

    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
