using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Enums;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Implementations;
using AbsenceManagementSystem.Services.Interfaces;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeLeaveService _employeeLeaveService;
        private readonly IEmployeeService _employeeService;

        public EmployeeLeaveController(ILogger<HomeController> logger, IEmployeeLeaveService employeeLeaveService, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeLeaveService = employeeLeaveService;
            _employeeService = employeeService;
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
            if(request.EndDate < request.StartDate)
            {
                ViewBag.Error = "Invalid start date and end date. Start date should not be higher the End date";
                return View();
            }

            //throw new Exception();
            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            request.EmployeeId = user.Id;
            request.EmployeeName = user.FullName;

            var startDate = new DateTime(request.StartDate.Year, request.StartDate.Month, request.StartDate.Day, 0, 0, 0);
            var tempDate = request.EndDate.AddDays(1);
            var endDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day, 0, 0, 0);
            request.EndDate = endDate;
            request.StartDate = startDate;
            request.NumberOfDaysOff = (int)(request.EndDate - request.StartDate).TotalDays;

            if (ModelState.IsValid)
            {
                request.EmployeeId = user.Id;
                var response = await _employeeLeaveService.RequestNewLeaveAsync(request);
                if (response.Succeeded)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("EmployeeDashboard", "Home");
                }

                ViewBag.Error = string.IsNullOrEmpty(response.Message) ? response.Errors : response.Message;
                return View();
            }
            return RedirectToAction("Error", ModelState.ValidationState.ToString());
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

        public async Task<IActionResult> PendingRequests()
        {
            HttpContext.Session.SetString("PageTitle", "Pending Requests");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var response = await _employeeLeaveService.GetPendingRequests();
            if (response != null)
            {
                return View(new PendingRequestsViewModel { Requests = response.ToList() });
            }
            return View(response);
        }

        //[HttpPost]
        public async Task<IActionResult> ApproveLeave(string requestId)
        {
            HttpContext.Session.SetString("PageTitle", "Pending Requests");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var payload = new UpdateEmployeeLeaveRequesDto()
            {
                Id = requestId,
                Status = LeaveStatus.Approved,
            };

            var response = await _employeeLeaveService.UpdateRequests(payload);
            if (response.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Error", response.Message);
        }

        public async Task<IActionResult> RejectLeave(string requestId)
        {
            HttpContext.Session.SetString("PageTitle", "Pending Requests");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var payload = new UpdateEmployeeLeaveRequesDto()
            {
                Id = requestId,
                Status = LeaveStatus.Rejected,
            };

            var response = await _employeeLeaveService.UpdateRequests(payload);
            if (response.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error", response.Message);
        }

        public async Task<IActionResult> LeaveEntitlement()
        {
            HttpContext.Session.SetString("PageTitle", "Leave Entitlement");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var response = await _employeeService.GetEmployeeLeaveEntitlementAsync();

            if (response.Succeeded)
            {
                var leaveEntitled = new LeaveEntitlementViewModel()
                {
                    ContractType = response.Data.ContractType,
                    EmployeeName = response.Data.EmployeeName,
                    LeaveBalance = response.Data.LeaveBalance,
                    TotalLeaveEntitled = response.Data.TotalLeaveEntitled,
                    TotalLeavePending = response.Data.TotalLeavePending,
                    TotalLeaveTaken = response.Data.TotalLeaveTaken
                };
                return View(leaveEntitled);
            }
            return RedirectToAction("Error", response.Message);
        }

        public async Task<IActionResult> DeleteLeaveRequest(string requestId)
        {
            HttpContext.Session.SetString("PageTitle", "Dashboard");

            var authenticatedUser = HttpContext.Session.GetString("User");
            var user = authenticatedUser != null ? JsonConvert.DeserializeObject<AuthenticatedUserDto>(authenticatedUser) : null;
            if (user == null)
                return RedirectToAction("Login", "Authentication");

            var response = await _employeeLeaveService.DeleteLeaveRequest(requestId);

            if (response != null && response.Succeeded)
            {
                ViewBag.Success = "Request successfully deleted!";
                return RedirectToAction("EmployeeDashboard", "Home");
            }

            ViewBag.Error = $"Failed: {response?.Message ?? "something went wrong"}";
            HttpContext.Session.SetString("newerror", $"Failed: {response?.Message ?? "something went wrong"}");
            //return View();
            return RedirectToAction("EmployeeDashboard", "Home");
            //return RedirectToAction("Error", response.Message);
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
