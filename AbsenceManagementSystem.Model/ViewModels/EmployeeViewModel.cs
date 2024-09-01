using AbsenceManagementSystem.Model.DTOs;

namespace AbsenceManagementSystem.Model.ViewModels
{
    public class EmployeeViewModel
    {
        public List<EmployeeDto> Employees { get; set; }
    }
    public class EmployeeLeaveViewModel
    {
        public List<EmployeeLeaveRequesResponseDto> EmployeeLeaveRequests { get; set; }
    }
}
