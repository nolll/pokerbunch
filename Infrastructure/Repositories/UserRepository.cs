using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserStorage _userStorage;
        private readonly IUserFactory _userFactory;
        private readonly IRawUserFactory _rawUserFactory;

        public UserRepository(
            IUserStorage userStorage,
            IUserFactory userFactory,
            IRawUserFactory rawUserFactory)
        {
            _userStorage = userStorage;
            _userFactory = userFactory;
            _rawUserFactory = rawUserFactory;
        }

        public User GetUserByEmail(string email)
        {
            var rawUser = _userStorage.GetUserByEmail(email);
            return _userFactory.Create(rawUser);
        }

        public User GetUserByToken(string token)
        {
            var rawUser = _userStorage.GetUserByToken(token);
            return _userFactory.Create(rawUser);
        }

        public User GetUserByName(string userName)
        {
            var rawUser = _userStorage.GetUserByName(userName);
            return _userFactory.Create(rawUser);
        }

        public User GetUserByCredentials(string userNameOrEmail, string password)
        {
            var rawUser = _userStorage.GetUserByCredentials(userNameOrEmail, password);
            return _userFactory.Create(rawUser);
        }

        public IList<User> GetUsers()
        {
            return _userStorage.GetUsers().Select(_userFactory.Create).ToList();
        }

        public bool UpdateUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            return _userStorage.UpdateUser(rawUser);
        }

        public int AddUser(User user)
        {
            var rawUser = _rawUserFactory.Create(user);
            return _userStorage.AddUser(rawUser);
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
            return _userStorage.SetSalt(user.UserName, salt);
        }

        public bool SetEncryptedPassword(User user, string encryptedPassword)
        {
            return _userStorage.SetEncryptedPassword(user.UserName, encryptedPassword);
        }

        public string GetToken(User user)
        {
            return _userStorage.GetToken(user.UserName);
        }

        public bool SetToken(User user, string token)
        {
            return _userStorage.SetToken(user.UserName, token);
        }
    }
}
