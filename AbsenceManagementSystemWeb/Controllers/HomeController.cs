using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Interfaces;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(AdminDashboard data)
        {
            var username = HttpContext.Session.GetString("Username");
            var authenticatedUser = HttpContext.Session.GetString("User");
            var role = HttpContext.Session.GetString("UserRole");
            HttpContext.Session.SetString("PageTitle", "Dashboard");

            if (authenticatedUser == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;

            var employees = await _employeeService.GetEmployeesAsync();
            if (employees != null)
            {
                data.Employees = employees.Take(5).OrderByDescending(x => x.DateCreated).ToList();
            }

            if(user != null)
            {
                data.Username = username;
            }

            if(role != null)
            {
                data.Role = role;
            }

            var employeesToGoOnLeaveSoon = await _employeeService.EmployeesToGoOnLeaveSoon();
            if(employeesToGoOnLeaveSoon != null)
            {
                data.EmployeesToGoOnLeaveSoon = employeesToGoOnLeaveSoon ?? new List<EmployeeLeavePredictResponse>();
            }

            return View(data);
        }

        public async Task<IActionResult> EmployeeDashboard()
        {
            EmployeeDashboard data = new();

            var username = HttpContext.Session.GetString("Username");
            var authenticatedUser = HttpContext.Session.GetString("User");
            var role = HttpContext.Session.GetString("UserRole");
            HttpContext.Session.SetString("PageTitle", "Dashboard");

            if (authenticatedUser == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;

            var employees = await _employeeService.GetEmployeeDashboardInfoAsync();
            if (employees != null)
            {
                data.LeaveRecords = employees.LeaveRecords;
            }

            if(user != null)
            {
                data.TotalLeaveRemaining = employees.TotalLeaveRemaining;
            }

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
