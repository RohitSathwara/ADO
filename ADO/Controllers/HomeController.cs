using Microsoft.AspNetCore.Mvc;

namespace ADO.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()  // Login page
        {
            return View();
        }

        public IActionResult Employees() // Employees CRUD page
        {
            return View("Employee");
        }

        public IActionResult UnauthorizedAccess()
        {
            return View();
        }
    }
}
