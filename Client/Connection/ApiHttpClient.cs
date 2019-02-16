using System.Net.Http;
using System.Net.Http.Headers;

namespace PokerBunch.Client.Connection
{
    public class ApiHttpClient : HttpClient
    {
        protected ApiHttpClient()
        {
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
