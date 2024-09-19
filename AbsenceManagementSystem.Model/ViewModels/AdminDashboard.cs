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
        public List<EmployeeLeavePredictResponse> EmployeesToGoOnLeaveSoon { get; set; }
    }

    public class EmployeeDashboard
    {
        public int TotalLeaveRemaining { get; set; }
        public List<EmployeeLeaveRequesResponse2Dto> LeaveRecords { get; set; }
    }

    public class EmployeeLeavePredictResponse
    {
        public float LeaveType { get; set; }
        public string EmployeeName { get; set; }
        public bool Status { get; set; }
    }
}
