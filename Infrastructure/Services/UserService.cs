using System;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
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

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var apiUser = ApiClient.Users.Get(nameOrEmail);
            return CreateUser(apiUser);
        }

        private User CreateUser(ApiUser u)
        {
            var role = (Role)Enum.Parse(typeof(Role), u.Role, true);
            return new User(u.Id, u.UserName, u.DisplayName, u.RealName, u.Email, role);
        }
    }
}