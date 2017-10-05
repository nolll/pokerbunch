using PokerBunch.Client.Clients;

namespace Infrastructure.Api.Services
{
    public abstract class BaseService
    {
        protected PokerBunchClient ApiClient { get; }

        protected BaseService(PokerBunchClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}