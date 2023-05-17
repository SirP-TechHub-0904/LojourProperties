using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LojourProperties.UI.Pages
{
    public class ResultModel : PageModel
    {
        public void OnGet(string error)
        {
            TempData["error"] = error;
        }
    }
}
