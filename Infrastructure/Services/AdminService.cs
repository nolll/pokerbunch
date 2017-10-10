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
            return ApiClient.Admin.ClearCache();
        }

        public string SendEmail()
        {
            return ApiClient.Admin.SendEmail();
        }
    }
}
