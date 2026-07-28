using System.Diagnostics;
using Denoy_INFASS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Denoy_INFASS2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("GetUser")]
        [HttpPost]
        public IActionResult GetUser(string Username, string Email, string Password, string ConfPass)
        {
            Users user = new Users();

            string[] Fields = { "Username", "Email", "Password", "ConfPass" };
            object[] Values =
            {
                Username, Email, Password, ConfPass

            };

            return Content(user.GenerateSQL("Users", Fields, Values) + "\n\n //View \n" + (user.ViewSQL("Users"))+ "\n\n //Update\n" 
                + (user.UpdateSQL("Users", "Password", Username, "Username", Username)) + "\n\n //Delete\n" + (user.DeleteSQL("Users", "Username", Username)));


        }

    }
}