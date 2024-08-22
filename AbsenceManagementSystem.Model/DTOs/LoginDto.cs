using System.ComponentModel.DataAnnotations;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Pls enter password")]
        public string Password { get; set; }
    }
}
