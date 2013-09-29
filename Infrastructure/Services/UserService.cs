using Core.Services;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserStorage _userStorage;

        public UserService(IUserStorage userStorage)
        {
            _userStorage = userStorage;
        }

        public bool IsUserNameAvailable(string userName)
        {
            var user = _userStorage.GetUserByName(userName);
            return user == null;
        }

        public bool IsEmailAvailable(string email)
        {
            var user = _userStorage.GetUserByEmail(email);
			return user == null;
        }
    }
}
