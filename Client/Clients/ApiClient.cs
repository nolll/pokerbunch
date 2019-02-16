using PokerBunch.Client.Connection;

namespace PokerBunch.Client.Clients
{
    public abstract class ApiClient
    {
        protected ApiConnection ApiConnection { get; }

        protected ApiClient(ApiConnection apiConnection)
        {
            ApiConnection = apiConnection;
        }
    }
}