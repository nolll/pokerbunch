using System.Net.Http.Headers;

namespace Infrastructure.Api.Connection
{
    public class BearerHttpClient : ApiHttpClient
    {
        public BearerHttpClient(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
