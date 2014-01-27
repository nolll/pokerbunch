using Application.Services.Interfaces;
using Core.Classes;
using Core.Repositories;
using Infrastructure.System;
using Web.Models.AuthModels;

namespace Web.Commands.AuthCommands
{
    public class LoginCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebContext _webContext;
        private readonly AuthLoginPostModel _postModel;

        public LoginCommand(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext,
            AuthLoginPostModel postModel)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            var user = GetLoggedInUser(_postModel.LoginName, _postModel.Password);
            if (user != null)
            {
                SetCookies(user, _postModel.RememberMe);
                return true;
            }
            AddError("There was something wrong with your username or password. Please try again.");
            return false;
        }

        private User GetLoggedInUser(string loginName, string password)
        {
            var user = _userRepository.GetByNameOrEmail(loginName);
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