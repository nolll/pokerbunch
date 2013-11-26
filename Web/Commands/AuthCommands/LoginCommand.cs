using Core.Classes;
using Core.Repositories;
using Core.Services;
using Infrastructure.System;

namespace Web.Commands.AuthCommands
{
    public class LoginCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebContext _webContext;
        private readonly string _loginName;
        private readonly string _password;
        private readonly bool _rememberMe;

        public LoginCommand(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext,
            string loginName, 
            string password, 
            bool rememberMe)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
            _loginName = loginName;
            _password = password;
            _rememberMe = rememberMe;
        }

        public override bool Execute()
        {
            var user = GetLoggedInUser(_loginName, _password);
            if (user != null)
            {
                SetCookies(user, _rememberMe);
                return true;
            }
            AddError("There was something wrong with your username or password. Please try again.");
            return false;
        }

        private User GetLoggedInUser(string loginName, string password)
        {
            var user = _userRepository.GetUserByNameOrEmail(loginName);
            var encryptedPassword = _encryptionService.Encrypt(password, user.Salt);
            return encryptedPassword == user.EncryptedPassword ? user : null;
        }

        private void SetCookies(User user, bool remember)
        {
            if (remember)
            {
                SetPersistentCookies(user);
            }
            else
            {
                SetSessionCookies(user);
            }
        }

        private void SetSessionCookies(User user)
        {
            _webContext.SetSessionCookie("token", user.Token);
        }

        private void SetPersistentCookies(User user)
        {
            _webContext.SetPersistentCookie("token", user.Token);
        }
    }
}