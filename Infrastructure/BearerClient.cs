using System.Net.Http.Headers;

namespace Infrastructure
{
    public class BearerClient : ApiClient
    {
        public BearerClient(string apiUrl, string token)
            : base(apiUrl)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
