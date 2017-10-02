using System.Net.Http.Headers;

namespace Infrastructure.Api.Connection
{
    public class BearerClient : ApiClient
    {
        public BearerClient(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
