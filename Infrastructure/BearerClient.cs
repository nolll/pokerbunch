using System.Net.Http.Headers;

namespace Infrastructure
{
    public class BearerClient : ApiClient
    {
        public BearerClient(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
