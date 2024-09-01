using AbsenceManagementSystem.Services.Implementations;
using AbsenceManagementSystem.Services.Interfaces;

namespace AbsenceManagementSystemWeb.Extensions
{
    public static class ServicesExtension
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {

            // Add Repository Injections Here 
            services.AddSingleton<IHttpRequestFactory, HttpRequestFactory>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
        }
    }
}
