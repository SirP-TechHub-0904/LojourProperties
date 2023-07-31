using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace LojourProperties.Web.Areas.Admin.Pages.V1.PropertyPage
{
    [Authorize(Roles = "mSuperAdmin,Admin")]
    public class GuideModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
