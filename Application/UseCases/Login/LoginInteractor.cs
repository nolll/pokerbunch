using System.Collections.Generic;
using Application.Services;
using Application.Urls;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.Login
{
    public class LoginInteractor : ILoginInteractor
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuth _auth;
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;

        public LoginInteractor(
            IUserRepository userRepository,
            IAuth auth,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository)
        {
            _userRepository = userRepository;
            _auth = auth;
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
        }

        public LoginResult Execute(LoginRequest request)
        {
            var user = GetLoggedInUser(request.LoginName, request.Password);

            var validator = new Validator();

            if (user != null)
            {
                var identity = CreateUserIdentity(user);
                _auth.SignIn(identity, request.RememberMe);
            }
            else
            {
                validator.AddError("There was something wrong with your username or password. Please try again.");
            }
            
            var returnUrl = new Url(request.ReturnUrl);
            return new LoginResult(validator, returnUrl);
        }

        private UserIdentity CreateUserIdentity(User user)
        {
            return new UserIdentity
            {
                Bunches = GetUserBunches(user),
                DisplayName = user.DisplayName,
                IsAdmin = user.IsAdmin,
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        private List<UserBunch> GetUserBunches(User user)
        {
            var homegames = _bunchRepository.GetByUser(user);
            var userBunches = new List<UserBunch>();

            if (homegames == null) return userBunches;

            foreach (var homegame in homegames)
            {
                var role = _bunchRepository.GetRole(homegame, user);
                var player = _playerRepository.GetByUserName(homegame, user.UserName);
                var userBunch = new UserBunch(homegame.Slug, role, player.DisplayName, player.Id);
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