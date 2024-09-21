using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Implementations;
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
        private readonly IEmployeeLeaveService _employeeLeaveService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IEmployeeLeaveService employeeLeaveService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _employeeLeaveService = employeeLeaveService;
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
            else
            {
                data.EmployeesToGoOnLeaveSoon =  new List<EmployeeLeavePredictResponse>();
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

            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var response = await _employeeLeaveService.GetEmployeeLeavesByEmployeeIdAsync(user.Id);

            var employees = await _employeeService.GetEmployeeDashboardInfoAsync();
            if (response != null)
            {
                data.LeaveRecords = response.Select(x => new EmployeeLeaveRequesResponse2Dto
                {
                    EmployeeId = x.EmployeeId,
                    EmployeeName = x.EmployeeName,
                    EndDate = x.EndDate,
                    NumberOfDaysOff = x.NumberOfDaysOff,
                    Id = x.Id,
                    LeaveType = x.LeaveType,
                    RequestDate = x.RequestDate,
                    StartDate = x.StartDate,
                    Status = x.Status
                }).ToList();// employees.LeaveRecords;
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
