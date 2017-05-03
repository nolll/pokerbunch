using System.Collections.Generic;
using Core.Entities;

namespace Core.Services
{
    public interface IUserService
    {
        User Current(string token);
        User Current();
        User GetById(string id);
        IList<User> List();
        User GetByNameOrEmail(string nameOrEmail);
        void Update(User user);
        string Add(User user, string password);
        void ChangePassword(string oldPassword, string newPassword, string repeat);
        void ResetPassword(string email);
    }
}