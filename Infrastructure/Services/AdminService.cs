using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApiConnection _api;

        public AdminService(ApiConnection api)
        {
            _api = api;
        }

        public string ClearCache()
        {
            var apiMessage = _api.Post<ApiMessage>(new ApiAdminClearCacheUrl());
            return apiMessage.Message;
        }

        public string SendEmail()
        {
            var apiMessage = _api.Post<ApiMessage>(new ApiAdminSendEmailUrl());
            return apiMessage.Message;
        }
    }
}
