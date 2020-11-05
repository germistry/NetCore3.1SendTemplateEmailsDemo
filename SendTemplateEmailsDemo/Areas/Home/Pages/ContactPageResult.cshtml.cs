using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SendTemplateEmailsDemo.Areas.Home.Pages
{
    [AllowAnonymous]
    public class ContactPageResultModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
