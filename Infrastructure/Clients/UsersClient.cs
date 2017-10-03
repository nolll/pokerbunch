using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class UsersClient : ApiClient
    {
        public PasswordClient Passwords { get; }

        public UsersClient(ApiConnection apiConnection) : base(apiConnection)
        {
            Passwords = new PasswordClient(apiConnection);
        }

        public ApiUser Current(string token)
        {
            return ApiConnection.Get<ApiUser>(token, new ApiUserProfileUrl());
        }

        public IList<ApiListUser> List()
        {
            return ApiConnection.Get<IList<ApiListUser>>(new ApiUsersUrl());
        }

        public ApiUser Get(string nameOrEmail)
        {
            return ApiConnection.Get<ApiUser>(new ApiUserUrl(nameOrEmail));
        }

        public void Update(ApiUser user)
        {
            ApiConnection.Post<ApiUser>(new ApiUserUrl(user.UserName), user);
        }

        public ApiUser Add(ApiUser user)
        {
            return ApiConnection.Post<ApiUser>(new ApiUsersUrl(), user);
        }
    }
}