using AbsenceManagementSystem.Model.Enums;

namespace AbsenceManagementSystem.Model.ViewModels
{
    public class LeaveEntitlementViewModel
    {
        public string EmployeeName { get; set; }
        public string ContractType { get; set; }
        public int TotalLeaveEntitled { get; set; }
        public int TotalLeaveTaken { get; set; }
        public int TotalLeavePending { get; set; }
        public int LeaveBalance { get; set; }
    }
}
