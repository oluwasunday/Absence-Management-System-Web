using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(AdminDashboard data)
        {
            var authenticatedUser = HttpContext.Session.GetString("User");
            if (authenticatedUser == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;

            return View(data);
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
