using System.Collections.Generic;
using Core.Classes;

namespace Core.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        User GetUserByToken(string token);
        User GetUserByName(string userName);
        User GetUserByCredentials(string userNameOrEmail, string password);
        IList<User> GetUsers();
        bool UpdateUser(User user);
        int AddUser(User user);
        string GetSalt(string userNameOrEmail);
        bool SetSalt(User user, string salt);
        bool SetEncryptedPassword(User user, string encryptedPassword);
        string GetToken(User user);
        bool SetToken(User user, string token);
    }
}