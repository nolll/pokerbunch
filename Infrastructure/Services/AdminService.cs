using Core.Services;
using Infrastructure.Api.Clients;

namespace Infrastructure.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly PokerBunchClient _apiClient;

        public AdminService(PokerBunchClient apiClient)
        {
            _apiClient = apiClient;
        }

        public string ClearCache()
        {
            var apiMessage = _apiClient.Admin.ClearCache();
            return apiMessage.Message;
        }

        public string SendEmail()
        {
            var apiMessage = _apiClient.Admin.SendEmail();
            return apiMessage.Message;
        }
    }
}
