using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;
using AbsenceManagementSystem.Services.Interfaces;

namespace AbsenceManagementSystem.Services.Implementations
{
    public class EmployeeLeaveService : IEmployeeLeaveService
    {
        private readonly IHttpRequestFactory _requestFactory;
        private const string baseUrl = "api/LeaveRequests";

        public EmployeeLeaveService(IHttpRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public async Task<IEnumerable<EmployeeLeaveRequesResponseDto>> GetAllPendingLeaveRequest()
        {
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeLeaveRequesResponseDto>>>(requestUrl: baseUrl);

            return response.Data;
        }

        public async Task<IEnumerable<EmployeeLeaveRequesResponseDto>> GetAllLeaveRequest()
        {
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeLeaveRequesResponseDto>>>(requestUrl: baseUrl);

            return response.Data;
        }

        public async Task<bool> UpdateLeaveRequests(UpdateLeaveRequesDto employeeLeave)
        {
            var response = await _requestFactory.UpdateRequestAsync<UpdateLeaveRequesDto, Response<bool>>(requestUrl: baseUrl, employeeLeave);

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
    }
}
