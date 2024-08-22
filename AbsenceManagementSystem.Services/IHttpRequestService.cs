namespace AbsenceManagementSystem.Services
{
    public interface IHttpRequestService
    {
        Task<TRes> GetRequestAsync<TRes>(string requestUrl, string baseUrl = null) where TRes : class;
        Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq content, string baseUrl = null)
            where TReq : class
            where TRes : class;
    }
}
