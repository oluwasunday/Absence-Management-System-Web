using AbsenceManagementSystem.Model.DTOs;

namespace AbsenceManagementSystem.Model.ViewModels
{
    public class AdminDashboard
    {
        public string UserId { get; set; }
        public int NumberOfEmployees { get; set; }
        public int EmployeesOnCasualLeave { get; set; }
        public int EmployeesOnSickLeave { get; set; }
        public int PendingLeave { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<EmployeeDto> Employees { get; set; }
    }
}
