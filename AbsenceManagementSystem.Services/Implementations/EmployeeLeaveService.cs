﻿using AbsenceManagementSystem.Model.DTOs;
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

        /*public async Task<Response<EmployeeDto>> AddNewEmployeeAsync(EmployeeDto employee)
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
        }*/

        public async Task<Response<EmployeeDto>> RequestNewLeaveAsync(EmployeeLeaveRequestDto request)
        {
            try
            {
                var response = await _requestFactory.PostRequestAsync<EmployeeLeaveRequestDto, Response<EmployeeDto>>(
                   requestUrl: baseUrl, request);

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

        public async Task<IEnumerable<EmployeeLeaveRequesResponseDto>> GetEmployeeLeavesByEmployeeIdAsync(string employeeId)
        {
            string leaveBaseUrl = $"api/LeaveRequests/{employeeId}";
            var response = await _requestFactory.GetRequestAsync<Response<IEnumerable<EmployeeLeaveRequesResponseDto>>>(requestUrl: leaveBaseUrl);

            return response.Data;
        }

        public async Task<List<EmployeeLeaveRequesResponseDto>> GetPendingRequests()
        {
            string leaveBaseUrl = $"api/LeaveRequests/allpendingrequests";
            var response = await _requestFactory.GetRequestAsync<Response<List<EmployeeLeaveRequesResponseDto>>>(requestUrl: leaveBaseUrl);

            return response.Data;
        }

        public async Task<Response<bool>> UpdateRequests(UpdateEmployeeLeaveRequesDto payload)
        {
            string leaveBaseUrl = $"api/LeaveRequests";
            var response = await _requestFactory.UpdateRequestAsync<UpdateEmployeeLeaveRequesDto, Response<bool>>(requestUrl: leaveBaseUrl, payload);

            return response;
        }
    }
}
