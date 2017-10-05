using Core.Services;
using PokerBunch.Client.Clients;

namespace Infrastructure.Api.Services
{
    public class AdminService : BaseService, IAdminService
    {
        public AdminService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public string ClearCache()
        {
            var apiMessage = ApiClient.Admin.ClearCache();
            return apiMessage.Message;
        }

        public string SendEmail()
        {
            var apiMessage = ApiClient.Admin.SendEmail();
            return apiMessage.Message;
        }
    }
}
