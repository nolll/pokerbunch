using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserStorage _userStorage;

        public UserRepository(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public User GetUserByEmail(string email)
        {
            return _userStorage.GetUserByEmail(email);
        }

        public User GetUserByToken(string token)
        {
            return _userStorage.GetUserByToken(token);
        }

        public User GetUserByName(string userName)
        {
            return _userStorage.GetUserByName(userName);
        }

        public User GetUserByCredentials(string userNameOrEmail, string password)
        {
            return _userStorage.GetUserByCredentials(userNameOrEmail, password);
        }

        public IList<User> GetUsers()
        {
            return _userStorage.GetUsers();
        }

        public bool UpdateUser(User user)
        {
            return _userStorage.UpdateUser(user);
        }

        public int AddUser(User user)
        {
            return _userStorage.AddUser(user);
        }

        public bool DeleteUser(User user)
        {
            throw new global::System.NotImplementedException();
        }

        public string GetSalt(string userNameOrEmail)
        {
            return _userStorage.GetSalt(userNameOrEmail);
        }

        public bool SetSalt(User user, string salt)
        {
            return _userStorage.SetSalt(user, salt);
        }

        public bool SetEncryptedPassword(User user, string encryptedPassword)
        {
            return _userStorage.SetEncryptedPassword(user, encryptedPassword);
        }

        public string GetToken(User user)
        {
            return _userStorage.GetToken(user);
        }

        public bool SetToken(User user, string token)
        {
            return _userStorage.SetToken(user, token);
        }
    }
}
