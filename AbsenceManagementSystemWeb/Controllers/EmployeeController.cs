﻿using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Interfaces;
using AbsenceManagementSystemWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("PageTitle", "Employees");
            var employees = await _employeeService.GetEmployeesAsync();
            if (employees != null)
            {
                
                return View(new EmployeeViewModel { Employees = employees.ToList() });
            }

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult AddNewEmployee()
        {
            HttpContext.Session.SetString("PageTitle", "Employees");
            return View();
        }
/*
        public IActionResult AddEmployee()
        {
            HttpContext.Session.SetString("PageTitle", "Employees");
            return View();
        }*/

        [HttpPost]
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
        }

        public async Task<IActionResult> EmployeeLeaves(string employeeId)
        {
            var id = employeeId;
            var response = await _employeeService.GetEmployeeLeavesByEmployeeIdAsync(employeeId);
            HttpContext.Session.SetString("PageTitle", "Employees");
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
