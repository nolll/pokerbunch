using System.Net.Http.Headers;

namespace Infrastructure
{
    public class BearerClient : ApiClient
    {
        public BearerClient(string apiHost, string token)
            : base(apiHost)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
