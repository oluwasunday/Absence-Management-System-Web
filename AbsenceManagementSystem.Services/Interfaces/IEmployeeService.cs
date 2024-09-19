using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;

namespace AbsenceManagementSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Response<EmployeeDto>> AddNewEmployeeAsync(EmployeeDto employee);
        Task<IEnumerable<EmployeeDto>> GetEmployeeLeavesByEmployeeIdAsync(string employeeId);
        Task<EmployeeDashboardDto> GetEmployeeDashboardInfoAsync();
        Task<List<EmployeeLeavePredictResponse>> EmployeesToGoOnLeaveSoon();
    }
}
