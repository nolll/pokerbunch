using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class UserClient : ApiClient
    {
        public PasswordClient Passwords { get; }

        public UserClient(ApiConnection apiConnection) : base(apiConnection)
        {
            Passwords = new PasswordClient(apiConnection);
        }

        public User Current(string token)
        {
            return ApiConnection.Get<User>(token, new ApiUserProfileUrl());
        }

        public IList<UserSmall> List()
        {
            return ApiConnection.Get<IList<UserSmall>>(new ApiUsersUrl());
        }

        public User Get(string nameOrEmail)
        {
            return ApiConnection.Get<User>(new ApiUserUrl(nameOrEmail));
        }

        public void Update(UserUpdate user)
        {
            ApiConnection.Post<User>(new ApiUserUrl(user.UserName), user);
        }

        public User Add(UserAdd user)
        {
            return ApiConnection.Post<User>(new ApiUsersUrl(), user);
        }
    }
}