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
        public static LoginResult Execute(
            IUserRepository userRepository,
            IAuth auth,
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            LoginRequest request)
        {
            var user = GetLoggedInUser(userRepository, request.LoginName, request.Password);

            if (user != null)
            {
                var identity = CreateUserIdentity(bunchRepository, playerRepository, user);
                auth.SignIn(identity, request.RememberMe);
            }
            else
            {
                throw new LoginException();
            }
            
            var returnUrl = new Url(request.ReturnUrl);
            return new LoginResult(returnUrl);
        }

        private static UserIdentity CreateUserIdentity(IBunchRepository bunchRepository, IPlayerRepository playerRepository, User user)
        {
            return new UserIdentity
            {
                Bunches = GetUserBunches(bunchRepository, playerRepository, user),
                DisplayName = user.DisplayName,
                IsAdmin = user.IsAdmin,
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        private static List<UserBunch> GetUserBunches(IBunchRepository bunchRepository, IPlayerRepository playerRepository, User user)
        {
            var homegames = bunchRepository.GetByUser(user);
            var userBunches = new List<UserBunch>();

            if (homegames == null) return userBunches;

            foreach (var bunch in homegames)
            {
                var role = bunchRepository.GetRole(bunch.Id, user.Id);
                var player = playerRepository.GetByUserName(bunch.Id, user.UserName);
                var userBunch = new UserBunch(bunch.Slug, role, player.DisplayName, player.Id);
                userBunches.Add(userBunch);
            }
            return userBunches;
        }

        private static User GetLoggedInUser(IUserRepository userRepository, string loginName, string password)
        {
            var user = userRepository.GetByNameOrEmail(loginName);
            if (user == null)
                return null;
            var encryptedPassword = EncryptionService.Encrypt(password, user.Salt);
            return encryptedPassword == user.EncryptedPassword ? user : null;
        }
    }
}