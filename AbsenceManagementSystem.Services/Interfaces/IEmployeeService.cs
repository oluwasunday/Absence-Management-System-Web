using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;

namespace AbsenceManagementSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<Response<EmployeeDto>> AddNewEmployeeAsync(EmployeeDto employee);
    }
}
