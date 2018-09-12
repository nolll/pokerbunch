using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Infrastructure.Api.FakeServices
{
    public class FakeUserService : IUserService
    {
        public User Current(string token)
        {
            return FakeData.Users.FirstOrDefault(o => o.Id == FakeData.CurrentUserId);
        }

        public IList<ListUser> List()
        {
            return FakeData.Users.Select(o => new ListUser(o.UserName, o.DisplayName)).ToList();
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            return FakeData.Users.FirstOrDefault(o => o.UserName == nameOrEmail || o.Email == nameOrEmail);
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }

        public string Add(User user, string password)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(string oldPassword, string newPassword, string repeat)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(string email)
        {
            throw new NotImplementedException();
        }
    }
}