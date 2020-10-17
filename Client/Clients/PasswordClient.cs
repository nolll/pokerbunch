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

        public void ResetPassword(PasswordReset passwordReset)
        {
            ApiConnection.Post(new ApiUserResetPasswordUrl(), passwordReset);
        }
    }
}