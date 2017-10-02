using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using Infrastructure.Api.Models.Request;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class UserService : IUserService
    {
        private readonly ApiConnection _api;

        public UserService(ApiConnection api)
        {
            _api = api;
        }

        public User Current(string token)
        {
            var apiUser = _api.Get<ApiUser>(token, new ApiUserProfileUrl());
            return CreateUser(apiUser);
        }

        public IList<ListUser> List()
        {
            var apiUsers = _api.Get<IList<ApiListUser>>(new ApiUsersUrl());
            return apiUsers.Select(CreateListUser).ToList();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var apiUser = _api.Get<ApiUser>(new ApiUserUrl(nameOrEmail));
            return CreateUser(apiUser);
        }

        public void Update(User user)
        {
            var userName = user.UserName;
            var postUser = new ApiUser(user);
            _api.Post<ApiUser>(new ApiUserUrl(userName), postUser);
        }

        public string Add(User user, string password)
        {
            var postUser = new ApiUser(user, password);
            var apiUser = _api.Post<ApiUser>(new ApiUsersUrl(), postUser);
            return apiUser.Id;
        }

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            var apiChangePassword = new ApiChangePassword(oldPassword, newPassword, repeat);
            _api.Post(new ApiUserChangePasswordUrl(), apiChangePassword);
        }

        public void ResetPassword(string email)
        {
            var apiResetPassword = new ApiResetPassword(email);
            _api.Post(new ApiUserResetPasswordUrl(), apiResetPassword);
        }

        private User CreateUser(ApiUser u)
        {
            var role = (Role)Enum.Parse(typeof(Role), u.Role, true);
            return new User(u.Id, u.UserName, u.DisplayName, u.RealName, u.Email, role);
        }

        private ListUser CreateListUser(ApiListUser u)
        {
            return new ListUser(u.UserName, u.DisplayName);
        }
    }
}