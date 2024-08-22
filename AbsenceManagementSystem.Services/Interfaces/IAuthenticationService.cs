using AbsenceManagementSystem.Model.DTOs;
using AbsenceManagementSystem.Model.Utilities;
using AbsenceManagementSystem.Model.ViewModels;

namespace AbsenceManagementSystem.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Response<LoginViewModel>> Login(LoginDto loginDto);
        Task<RegisterDto> Register(RegisterDto registerdto);
    }
}
