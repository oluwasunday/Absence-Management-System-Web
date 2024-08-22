using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AbsenceManagementSystem.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IWebHostEnvironment _web;
        private string _url;

        public HttpRequestService(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            //_web = web;
            _url = configuration["BaseUrl"];
        }

        public async Task<TRes> GetRequestAsync<TRes>(string requestUrl, string baseUrl = null) where TRes : class
        {
            var client = CreateClient(baseUrl);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            return await GetResponseResultAsync<TRes>(client, request);
        }

        public async Task<TRes> PostRequestAsync<TReq, TRes>(string requestUrl, TReq content, string baseUrl = null) where TRes : class where TReq : class
        {
            var client = CreateClient(baseUrl);
            var reqContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl) { Content = reqContent };
            return await GetResponseResultAsync<TRes>(client, request);
        }

        private async Task<TRequest> GetResponseResultAsync<TRequest>(HttpClient client, HttpRequestMessage request) where TRequest : class
        {
            var response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TRequest>(responseString);
            return result;
        }


        private HttpClient CreateClient(string baseUrl = null)
        {
            baseUrl ??= _url;
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(baseUrl);
            var token = _httpContextAccessor.HttpContext.Session.GetString("access_token");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
            return client;
        }
    }
}
