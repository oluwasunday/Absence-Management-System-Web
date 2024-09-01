using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.ViewModels;

namespace AbsenceManagementSystem.Services.Interfaces
{
    public interface IEmployeeLeaveService
    {
        Task<IEnumerable<EmployeeLeaveRequesResponseDto>> GetAllLeaveRequest();
        Task<IEnumerable<EmployeeLeaveRequesResponseDto>> GetAllPendingLeaveRequest();
        Task<bool> UpdateLeaveRequests(UpdateLeaveRequesDto employeeLeave);
    }
}
