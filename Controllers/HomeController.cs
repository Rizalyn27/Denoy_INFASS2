using Denoy_INFASS2.Models;
using Denoy_INFASS2.ViewModels;
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


        public IActionResult Users()
        {
            return View();
        }

        [HttpPost]
        [Route("GetUser")]
        public IActionResult GetUser(
            string username,
            string email,
            string password,
            string confPassword)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            string sql = @"
                INSERT INTO Users
                (Username, Email, Password, ConfirmPassword)
                VALUES
                (@Username, @Email, @Password, @ConfirmPassword)";

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@ConfirmPassword", confPassword);

            connection.Open();
            command.ExecuteNonQuery();

            return Content("User registered successfully!");
        }

        [HttpPost]
        [Route("View")]
        public IActionResult View()
        {
            var UserList = new List<UsersViewModel>();

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            string sql = @"
                SELECT * FROM Users";

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand(sql, connection);
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var user = new UsersViewModel
                {
                    Username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : string.Empty,
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                    Password = reader["Password"] != DBNull.Value ? reader["Password"].ToString() : string.Empty
                };

                UserList.Add(user);
            }   

            return View(UserList);
        }
    }
}