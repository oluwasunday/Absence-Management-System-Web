namespace AbsenceManagementSystem.Services.Interfaces
{
    public interface IHttpRequestFactory
    {
        Task<TRes> DeleteRequestAsync<TRes>(string requestUrl, string baseUrl = null) where TRes : class;
        Task<TRes> GetRequestAsync<TRes>(string requestUrl, string baseUrl = null) where TRes : class;
        Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq content, string baseUrl = null)
            where TReq : class
            where TRes : class;
        Task<TRes> UpdateRequestAsync<TReq, TRes>(string requestUrl, TReq content, string baseUrl = null)
            where TReq : class
            where TRes : class;
    }
}
