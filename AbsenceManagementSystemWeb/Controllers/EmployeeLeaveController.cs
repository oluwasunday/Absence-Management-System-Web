using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Implementations;
using AbsenceManagementSystem.Services.Interfaces;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public async Task<IActionResult> AddNewLeaveRequest()
        {
            HttpContext.Session.SetString("PageTitle", "Add Employees");
            
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddNewLeaveRequest(EmployeeLeaveRequestDto request)
        {
            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            if (ModelState.IsValid)
            {
                request.EmployeeId = user.Id;
                var response = await _employeeLeaveService.RequestNewLeaveAsync(request);
                if (response.Succeeded)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("EmployeeLeaves");
                }

                ViewBag.Error = response.Message;
                return View();
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> EmployeeLeaves()
        {
            HttpContext.Session.SetString("PageTitle", "Employee Leave Requests");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var response = await _employeeLeaveService.GetEmployeeLeavesByEmployeeIdAsync(user.Id);
            if (response != null)
            {
                return View(new EmployeeLeaveRequestViewModel { Requests = response.ToList() });
            }
            return View(response);
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
