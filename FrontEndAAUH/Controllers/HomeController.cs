using FrontEndAAUH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace FrontEndAAUH.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration) {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index() {
            var connString = _configuration.GetConnectionString("DefaultConnection");
            ViewBag.ConnectionString = connString;
            bool connectStringOk = false;

            try {
                var con = new SqlConnectionStringBuilder(connString);
                using (SqlConnection conn = new SqlConnection(connString)) {
                    conn.Open();
                    connectStringOk = (conn.State == ConnectionState.Open);
                }
            } catch (Exception ex){
                Console.WriteLine(ex);
                connectStringOk = false;
            }
            Console.WriteLine(connectStringOk);
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
