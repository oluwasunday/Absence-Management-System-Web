using AbsenceManagementSystem.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ContractType ContractType { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public int TotalHolidayEntitlement { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
    }

    public class EmployeeDashboardDto
    {
        public int TotalLeaveRemaining { get; set; }
        public List<EmployeeLeaveRequesResponse2Dto> LeaveRecords { get; set; }
    }

    public class EmployeeLeaveRequesResponse2Dto
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public int NumberOfDaysOff { get; set; }
        public LeaveTypes LeaveType { get; set; }
        public LeaveStatus Status { get; set; }
        public bool WillBeAbsent { get; set; }
    }
}
