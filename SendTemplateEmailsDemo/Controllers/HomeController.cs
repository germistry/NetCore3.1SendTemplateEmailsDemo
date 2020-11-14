using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using SendTemplateEmailsDemo.Models;
using SendTemplateEmailsDemo.ViewModels;

namespace SendTemplateEmailsDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailService _emailService;
        private readonly string _templatesPath;

        public HomeController(ILogger<HomeController> logger, IEmailService emailService,
            IConfiguration pathConfig)
        {
            _logger = logger;
            _emailService = emailService;
            _templatesPath = pathConfig["Path:Templates"];
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ContactMVC()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactMVC(ContactMVC vm)
        {
            if (!ModelState.IsValid)
                return View();
            
            string path = Path.Combine(_templatesPath);
            string template = "ContactTemplate.html";
            string FullPath = Path.Combine(path, template);

            StreamReader str = new StreamReader(FullPath);
            string mailText = str.ReadToEnd();
            str.Close();
            mailText = mailText.Replace("[fromEmail]", vm.Email).Replace("[fromName]", vm.Name).Replace("[contactMessage]", vm.Message);

            await _emailService.SendAsync("testmailbox@test.com", vm.Subject, mailText, true);

            return RedirectToAction("ContactResultMVC");
        }
        [HttpGet]
        public IActionResult ContactResultMVC()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
