using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Interfaces;

namespace AbsenceManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpRequestFactory _requestFactory;
        private const string baseUrl = "api/Employees";
        //private const string leaveBaseUrl = "api/LeaveRequests";

        public EmployeeService(IHttpRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeDto>>>(requestUrl: baseUrl);

            return response.Data;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeeLeavesByEmployeeIdAsync(string employeeId)
        {
            string leaveBaseUrl = $"api/LeaveRequests/{employeeId}";
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeDto>>>(requestUrl: leaveBaseUrl);

            return response.Data;
        }

        public async Task<Response<EmployeeDto>> AddNewEmployeeAsync(EmployeeDto employee)
        {
            try
            {
                var response = await _requestFactory.PostRequestAsync<EmployeeDto, Response<EmployeeDto>>(
                   requestUrl: baseUrl, employee);

                return response;
            }
            catch (Exception ex)
            {
                return new Response<EmployeeDto> 
                {
                    Message = $"{ex.Message} - {ex.StackTrace}",
                    Succeeded = false
                };
            }
            
        }

        public async Task<EmployeeDashboardDto> GetEmployeeDashboardInfoAsync()
        {
            var response = await _requestFactory.GetRequestAsync<Response<EmployeeDashboardDto>>(requestUrl: baseUrl + "/employeedashboard");

            return response.Data;
        }

        public async Task<List<EmployeeLeavePredictResponse>> EmployeesToGoOnLeaveSoon()
        {
            string apiUrl = $"api/AbsencePrediction";
            var response = await _requestFactory.GetRequestAsync<List<EmployeeLeavePredictResponse>>(requestUrl: apiUrl);

            return response;
        }

        public async Task<Response<LeaveEntitlementViewModel>> GetEmployeeLeaveEntitlementAsync()
        {
            var response = await _requestFactory.GetRequestAsync<Response<LeaveEntitlementViewModel>>(requestUrl: baseUrl + "/employeeleaveentitlement");

            return response;
        }
    }
}
