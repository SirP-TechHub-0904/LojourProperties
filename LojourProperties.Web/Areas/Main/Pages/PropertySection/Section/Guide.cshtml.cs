using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LojourProperties.Web.Areas.Main.Pages.PropertySection.Section
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class GuideModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
