using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Services.Interfaces;

namespace AbsenceManagementSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpRequestFactory _requestFactory;
        private const string baseUrl = "api/Employees";

        public EmployeeService(IHttpRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeDto>>>(requestUrl: baseUrl);

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
