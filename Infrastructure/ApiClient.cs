using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Infrastructure
{
    public class ApiClient : HttpClient
    {
        protected ApiClient(string apiUrl)
        {
            BaseAddress = new Uri(apiUrl);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
