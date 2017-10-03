using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Models;
using Infrastructure.Api.Models.Request;

namespace Infrastructure.Api.Services
{
    public class UserService : IUserService
    {
        private readonly PokerBunchClient _api;

        public UserService(PokerBunchClient api)
        {
            _api = api;
        }

        public User Current(string token)
        {
            var apiUser = _api.Users.Current(token);
            return CreateUser(apiUser);
        }

        public IList<ListUser> List()
        {
            var apiUsers = _api.Users.List();
            return apiUsers.Select(CreateListUser).ToList();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var apiUser = _api.Users.Get(nameOrEmail);
            return CreateUser(apiUser);
        }

        public void Update(User user)
        {
            var apiUser = new ApiUser(user);
            _api.Users.Update(apiUser);
        }

        public string Add(User user, string password)
        {
            var postUser = new ApiUser(user, password);
            var apiUser = _api.Users.Add(postUser);
            return apiUser.Id;
        }

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            var apiChangePassword = new ApiChangePassword(oldPassword, newPassword, repeat);
            _api.Users.Passwords.ChangePassword(apiChangePassword);
        }

        public void ResetPassword(string email)
        {
            var apiResetPassword = new ApiResetPassword(email);
            _api.Users.Passwords.ResetPassword(apiResetPassword);
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