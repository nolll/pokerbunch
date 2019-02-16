using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class PasswordClient : ApiClient
    {
        public PasswordClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public void ChangePassword(PasswordChange passwordChange)
        {
            ApiConnection.Post(new ApiUserChangePasswordUrl(), passwordChange);
        }

        public void ResetPassword(PasswordReset passwordReset)
        {
            ApiConnection.Post(new ApiUserResetPasswordUrl(), passwordReset);
        }
    }
}