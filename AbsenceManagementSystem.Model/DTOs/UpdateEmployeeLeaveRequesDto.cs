using AbsenceManagementSystem.Model.Enums;

namespace AbsenceManagementSystem.Model.DTOs
{
    public class UpdateEmployeeLeaveRequesDto
    {
        public string Id { get; set; }
        public LeaveStatus Status { get; set; }
    }
}
