using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HuoltoWebApp.Pages
{
    [Authorize]
    public class EtusivuModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
