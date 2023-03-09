using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojourProperties.Web.Pages
{
    public class ResultModel : PageModel
    {
        public void OnGet(string error)
        {
            TempData["error"] = error;
        }
    }
}
