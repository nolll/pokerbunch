using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class UserClient : ApiClient
    {
        public UserClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public User Current(string token)
        {
            return ApiConnection.Get<User>(token, new ApiUserProfileUrl());
        }

        public User Get(string nameOrEmail)
        {
            return ApiConnection.Get<User>(new ApiUserUrl(nameOrEmail));
        }
    }
}