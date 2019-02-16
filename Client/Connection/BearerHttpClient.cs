using System.Net.Http.Headers;

namespace PokerBunch.Client.Connection
{
    public class BearerHttpClient : ApiHttpClient
    {
        public BearerHttpClient(string token)
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
