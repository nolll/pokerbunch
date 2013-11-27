using Core.Repositories;
using Core.Services;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUserNameAvailable(string userName)
        {
            var user = _userRepository.GetByNameOrEmail(userName);
            return user == null;
        }

        public bool IsEmailAvailable(string email)
        {
            var user = _userRepository.GetByNameOrEmail(email);
			return user == null;
        }
    }
}
