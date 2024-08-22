using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AbsenceManagementSystemWeb.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public static string role = string.Empty;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginDto login)
        {

            if (ModelState.IsValid)
            {
                var response = await _authenticationService.Login(login);
                var result = response.Data;

                if (result == null || !response.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, response.Message = "Invalid Credentials" ?? response.Message);
                    return View();
                }

                ViewBag.UserDetails = result;

                var user = new LoginResponseDto()
                {
                    Id = result.Claims.ElementAt(0).Value,
                    FirstName = result.Claims.ElementAt(2).Value,
                    LastName = result.Claims.ElementAt(3).Value,
                    Avatar = result.Claims.ElementAt(4).Value,
                    Token = result.Token
                };

                ViewBag.UserInfo = user;

                var Role = result.Claims.ElementAt(4).Value;
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                TempData["Username"] = $"{user.FirstName}";
                TempData["Role"] = $"{Role}";
                if (Role == null)
                {
                    ModelState.AddModelError(string.Empty, "Unable to log you in at this time.");
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home", result.AdminDashboard);
                }
            }
            ViewBag.Error = "Error occur, pls check all required fields and try again";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("HotelUser");
            Response.Cookies.Delete("User");
            if (Request.Cookies["user"] != null)
            {
                Response.Cookies.Delete("user");
            }
            HttpContext.Session.Remove("User");
            return RedirectToAction("Login", "Authentication");
        }
    }
}
