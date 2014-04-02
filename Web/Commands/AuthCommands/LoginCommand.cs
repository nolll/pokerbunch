using System.Collections.Generic;
using Application.Services;
using Core.Classes;
using Core.Repositories;
using Web.Models.AuthModels;
using Web.Services;

namespace Web.Commands.AuthCommands
{
    public class LoginCommand : Command
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IWebContext _webContext;
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IHomegameRepository _homegameRepository;
        private readonly AuthLoginPostModel _postModel;
        
        public LoginCommand(
            IUserRepository userRepository,
            IEncryptionService encryptionService,
            IWebContext webContext,
            IFormsAuthenticationService formsAuthenticationService,
            IHomegameRepository homegameRepository,
            AuthLoginPostModel postModel)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _webContext = webContext;
            _formsAuthenticationService = formsAuthenticationService;
            _homegameRepository = homegameRepository;
            _postModel = postModel;
        }

        public override bool Execute()
        {
            var user = GetLoggedInUser(_postModel.LoginName, _postModel.Password);
            
            if (user != null)
            {
                var identity = GetUserIdentity(user);

                _formsAuthenticationService.SignIn(identity, _postModel.RememberMe);
                
                SetCookies(user, _postModel.RememberMe);
                return true;
            }
            AddError("There was something wrong with your username or password. Please try again.");
            return false;
        }

        private UserIdentity GetUserIdentity(User user)
        {
            var identity = new UserIdentity
                {
                    Bunches = GetUserBunches(user),
                    DisplayName = user.DisplayName,
                    IsAdmin = user.IsAdmin,
                    UserId = user.Id,
                    UserName = user.UserName
                };
            return identity;
        }

        private List<UserBunch> GetUserBunches(User user)
        {
            var homegames = _homegameRepository.GetByUser(user);
            var userBunches = new List<UserBunch>();
            if (homegames != null)
            {
                foreach (var homegame in homegames)
                {
                    var role = _homegameRepository.GetHomegameRole(homegame, user);
                    var userBunch = new UserBunch
                        {
                            Slug = homegame.Slug,
                            Role = role
                        };
                    userBunches.Add(userBunch);
                }
            }
            return userBunches;
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