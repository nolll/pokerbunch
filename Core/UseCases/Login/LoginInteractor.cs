using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.Login
{
    public class LoginInteractor
    {
        private readonly IUserRepository _userRepository;

        public LoginInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public LoginResult Execute(LoginRequest request)
        {
            var user = GetLoggedInUser(request.LoginName, request.Password);

            if (user == null)
                throw new LoginException();
            return new LoginResult(user.UserName);
        }

        private User GetLoggedInUser(string loginName, string password)
        {
            var user = _userRepository.GetByNameOrEmail(loginName);
            if (user == null)
                return null;
            var encryptedPassword = EncryptionService.Encrypt(password, user.Salt);
            return encryptedPassword == user.EncryptedPassword ? user : null;
        }
    }
}