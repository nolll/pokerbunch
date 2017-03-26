using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly ApiConnection _api;

        public AdminService(ApiConnection api)
        {
            _api = api;
        }

        public string ClearCache()
        {
            var apiMessage = _api.Post<ApiMessage>(Url.AdminClearCache);
            return apiMessage.Message;
        }

        public string SendEmail()
        {
            var apiMessage = _api.Post<ApiMessage>(Url.AdminSendEmail);
            return apiMessage.Message;
        }

        public class ApiMessage
        {
            [UsedImplicitly]
            public string Message { get; set; }

            public ApiMessage(string message)
            {
                Message = message;
            }

            public ApiMessage()
            {
            }
        }
    }
}
