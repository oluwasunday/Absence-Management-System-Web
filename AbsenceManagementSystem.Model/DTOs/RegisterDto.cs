using System.ComponentModel.DataAnnotations;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [Range(18, int.MaxValue, ErrorMessage = "Age limit is required")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(11, ErrorMessage = "Phone number must be 11 characters")]
        public string PhoneNumber { get; set; }
    }
}
