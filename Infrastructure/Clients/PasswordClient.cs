using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using Infrastructure.Api.Models.Request;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class PasswordClient : ApiClient
    {
        public PasswordClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public void ChangePassword(ApiChangePassword apiChangePassword)
        {
            ApiConnection.Post(new ApiUserChangePasswordUrl(), apiChangePassword);
        }

        public void ResetPassword(ApiResetPassword apiResetPassword)
        {
            ApiConnection.Post(new ApiUserResetPasswordUrl(), apiResetPassword);
        }
    }
}