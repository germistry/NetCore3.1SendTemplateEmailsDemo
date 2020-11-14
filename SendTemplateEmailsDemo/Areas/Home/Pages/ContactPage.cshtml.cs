using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using NETCore.MailKit.Core;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SendTemplateEmailsDemo.Areas.Home.Pages
{
    [AllowAnonymous]
    public class ContactPageModel : PageModel
    {
        
        private readonly IEmailService _emailService;
        private readonly string _templatesPath;

        public ContactPageModel(IEmailService emailService,
            IConfiguration pathConfig)
        {
            _emailService = emailService;
            _templatesPath = pathConfig["Path:Templates"];
        }

        [BindProperty]
        public InputModel Input { get; set; }
        
        public class InputModel
        {
            [Required]
            [StringLength(50)]
            public string Name { get; set; }
            [Required]
            [StringLength(250)]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [StringLength(100)]
            public string Subject { get; set; }
            [Required]
            public string Message { get; set; }
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
               return Page();

           
            string path = Path.Combine(_templatesPath);
            string template = "ContactTemplate.html";
            string FullPath = Path.Combine(path, template);

            StreamReader str = new StreamReader(FullPath);
            string mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[fromEmail]", Input.Email).Replace("[fromName]", Input.Name).Replace("[contactMessage]", Input.Message);

            await _emailService.SendAsync("testmailbox@test.com", Input.Subject, mailText, true);
                       
            return RedirectToPage("ContactPageResult");
        }
    }
}