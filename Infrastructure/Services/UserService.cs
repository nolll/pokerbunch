using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using User = Core.Entities.User;
using ApiUser = PokerBunch.Client.Models.Response.User;

namespace Infrastructure.Api.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public User Current(string token)
        {
            var apiUser = ApiClient.Users.Current(token);
            return CreateUser(apiUser);
        }

        public IList<ListUser> List()
        {
            var apiUsers = ApiClient.Users.List();
            return apiUsers.Select(CreateListUser).ToList();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var apiUser = ApiClient.Users.Get(nameOrEmail);
            return CreateUser(apiUser);
        }

        public void Update(User user)
        {
            var apiUser = new UserUpdate(user.UserName, user.DisplayName, user.RealName, user.Email);
            ApiClient.Users.Update(apiUser);
        }

        public string Add(User user, string password)
        {
            var postUser = new UserAdd(user.UserName, user.DisplayName, user.Email, password);
            var apiUser = ApiClient.Users.Add(postUser);
            return apiUser.Id;
        }

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            var apiChangePassword = new PasswordChange(oldPassword, newPassword, repeat);
            ApiClient.Users.Passwords.ChangePassword(apiChangePassword);
        }

        public void ResetPassword(string email)
        {
            var apiResetPassword = new PasswordReset(email);
            ApiClient.Users.Passwords.ResetPassword(apiResetPassword);
        }

        private User CreateUser(ApiUser u)
        {
            var role = (Role)Enum.Parse(typeof(Role), u.Role, true);
            return new User(u.Id, u.UserName, u.DisplayName, u.RealName, u.Email, role);
        }

        private ListUser CreateListUser(UserSmall u)
        {
            return new ListUser(u.UserName, u.DisplayName);
        }
    }
}