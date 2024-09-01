using AbsenceManagementSystem.Model.Enums;

namespace AbsenceManagementSystem.Model.ViewModels
{
    public class UpdateLeaveRequesDto
    {
        public string Id { get; set; }
        public LeaveStatus Status { get; set; }
    }
}
