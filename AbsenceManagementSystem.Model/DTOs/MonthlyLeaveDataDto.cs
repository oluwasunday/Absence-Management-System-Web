namespace AbsenceManagementSystem.Model.DTOs
{
    public class MonthlyLeaveDataDto
    {
        public string Month { get; set; }
        public int LeaveCount { get; set; }
    }
    public class PieChartLeaveDataDto
    {
        public string LeaveType { get; set; }
        public int LeaveCount { get; set; }
    }
}
