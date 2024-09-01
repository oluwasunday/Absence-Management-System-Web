using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Interfaces;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeLeaveService _employeeLeaveService;

        public EmployeeLeaveController(ILogger<HomeController> logger, IEmployeeLeaveService employeeLeaveService)
        {
            _logger = logger;
            _employeeLeaveService = employeeLeaveService;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("PageTitle", "EmployeeLeaves");
            var requests = await _employeeLeaveService.GetAllLeaveRequest();
            if (requests != null)
            {
                return View(new EmployeeLeaveViewModel { EmployeeLeaveRequests = requests.ToList() });
            }

            return View();
        }

        public async Task<IActionResult> AllPendingLeaveRequests()
        {
            HttpContext.Session.SetString("PageTitle", "EmployeeLeaves");
            var requests = await _employeeLeaveService.GetAllPendingLeaveRequest();
            if (requests != null)
            {
                return View(new EmployeeLeaveViewModel { EmployeeLeaveRequests = requests.ToList() });
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLeaveRequest(UpdateLeaveRequesDto dto)
        {
            HttpContext.Session.SetString("PageTitle", "EmployeeLeaves");
            var response = await _employeeLeaveService.UpdateLeaveRequests(dto);
            if (response != null)
            {
                return View(new Response<bool> { Data = response });
            }

            return View();
        }
/*
        public IActionResult AddEmployee()
        {
            HttpContext.Session.SetString("PageTitle", "Employees");
            return View();
        }*/

       /* [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddNewEmployee(EmployeeDto employee)
        {
            HttpContext.Session.SetString("PageTitle", "Employees");
            if (ModelState.IsValid)
            {
                var response = await _employeeService.AddNewEmployeeAsync(employee);
                if (response.Succeeded)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Employee");
                }

                ViewBag.Error = response.Message;
                return View();
            }
            return RedirectToAction("Error");
        }*/

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
