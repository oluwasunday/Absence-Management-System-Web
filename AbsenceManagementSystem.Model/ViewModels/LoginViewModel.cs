using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AbsenceManagementSystem.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter Password To Continue")]
        public string Password { get; set; }
        public string Id { get; set; }
        public string Token { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public AdminDashboard AdminDashboard { get; set; }
    }
    public class AdminDashboard
    {
        public string UserId { get; set; }
        public int NumberOfEmployees { get; set; }
        public int EmployeesOnCasualLeave { get; set; }
        public int EmployeesOnSickLeave { get; set; }
        public int PendingLeave { get; set; }
    }
}
