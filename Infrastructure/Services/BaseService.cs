using Infrastructure.Api.Clients;

namespace Infrastructure.Api.Services
{
    public class BaseService
    {
        protected PokerBunchClient ApiClient { get; }

        public BaseService(PokerBunchClient apiClient)
        {
            ApiClient = apiClient;
        }
    }
}