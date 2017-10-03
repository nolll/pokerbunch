using Infrastructure.Api.Connection;

namespace Infrastructure.Api.Clients
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