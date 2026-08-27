    using Denoy_INFASS2.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using System.Diagnostics;

namespace Denoy_INFASS2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
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
        [Route("Register")]
        public IActionResult Register(
         string username,
         string email,
         string password,
         string confPassword)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            var userModel = new Users();

            string[] fields = { "Username", "Email", "Password", "ConfirmPassword" };
            object[] values = { username, email, password, confPassword };

            string sql = userModel.GenerateSQL("Users", fields, values);

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            command.ExecuteNonQuery();

            return Content("User registered successfully!");
        }

        [HttpPost]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            var userList = new List<Users>();
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            var userModel = new Users();
            string[] fields = { "Id", "Username", "Email", "Password" };
            string sql = userModel.ViewSQL("Users", fields);

            using SqlConnection connection = new SqlConnection(connectionString);
            using SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var user = new Users
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Username = reader["Username"].ToString(),
                    Email = reader["Email"].ToString(),
                    Password = reader["Password"].ToString()
                };
                userList.Add(user);
            }

            return Json(userList);
        }

        public IActionResult Users()
        {
            return View();
        }
    }
}