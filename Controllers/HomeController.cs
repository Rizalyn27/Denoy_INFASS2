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

        [HttpPost]
        [Route("getUser")]
        public IActionResult GetUser(string username, string email, string password, string confPassword)
        {
            Users user = new Users();

            string[] Fields = { "Username", "Email", "Password", "ConfPass" };
            object[] Values =
            {
                username, email, password, confPassword

            };

            string[] viewFields = { "*" };

            string[] updateFields = { "Password" };
            object[] updateValues = { email};

            string[] deleteFields = { "Password" };
            object[] deleteValues = { password };

            object[] conditionvalue = { username };


            return Content(
                user.GenerateSQL("User", Fields, Values) + "\n\n" +
                user.ViewSQL("User", viewFields) + "\n\n" +
                user.UpdateSQL("User", updateFields, updateValues, "Username", conditionvalue ) + "\n\n" +
                user.DeleteSQL("User", deleteFields, deleteValues)
            );

        }
    }
}