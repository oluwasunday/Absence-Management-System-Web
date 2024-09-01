using AbsenceManagementSystem.Model.Enums;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class EmployeeLeaveRequesResponseDto
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
    }
}
