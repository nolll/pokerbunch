using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.Login
{
    public class LoginInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public LoginInteractor(IUserRepository userRepository, IAuth auth, IBunchRepository bunchRepository, IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _auth = auth;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public LoginResult Execute(LoginRequest request)
        {
            var user = GetLoggedInUser(request.LoginName, request.Password);

            if (user != null)
            {
                var identity = CreateUserIdentity(user);
                _auth.SignIn(identity, request.RememberMe);
            }
            else
            {
                throw new LoginException();
            }
            
            var returnUrl = new Url(request.ReturnUrl);
            return new LoginResult(returnUrl);
        }

        private UserIdentity CreateUserIdentity(User user)
        {
            return new UserIdentity
            {
                Bunches = GetUserBunches(user.Id),
                DisplayName = user.DisplayName,
                IsAdmin = user.IsAdmin,
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        private List<UserBunch> GetUserBunches(int userId)
        {
            var homegames = _bunchRepository.GetByUserId(userId);
            var userBunches = new List<UserBunch>();

            if (homegames == null) return userBunches;

            foreach (var bunch in homegames)
            {
                var player = _playerRepository.GetByUserId(bunch.Id, userId);
                var userBunch = new UserBunch(bunch.Slug, player.Role, player.DisplayName, player.Id);
                userBunches.Add(userBunch);
            }
            return userBunches;
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