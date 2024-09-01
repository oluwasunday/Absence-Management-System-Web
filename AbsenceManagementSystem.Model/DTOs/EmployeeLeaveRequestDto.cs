using AbsenceManagementSystem.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class EmployeeLeaveRequestDto
    {
        [Required]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDaysOff { get; set; }
        public LeaveTypes LeaveType { get; set; }
    }
}
