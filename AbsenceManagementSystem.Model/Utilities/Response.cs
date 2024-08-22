using System.Security.Claims;

namespace AbsenceManagementSystem.Model.Utilities
{
    public class Response<T> where T : class
    {
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
        public Claim Claim { get; set; }
    }
    
}
