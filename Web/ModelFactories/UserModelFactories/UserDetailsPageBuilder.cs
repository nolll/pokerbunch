using Application.Services;
using Application.Urls;
using Application.UseCases.AppContext;
using Core.Repositories;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.PageBaseModels;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserDetailsPageBuilder : IUserDetailsPageBuilder
    {
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;
        private readonly IAppContextInteractor _appContextInteractor;

        public UserDetailsPageBuilder(
            IAvatarModelFactory avatarModelFactory, 
            IAuth auth,
            IUserRepository userRepository,
            IAppContextInteractor appContextInteractor)
        {
            _avatarModelFactory = avatarModelFactory;
            _auth = auth;
            _userRepository = userRepository;
            _appContextInteractor = appContextInteractor;
        }

        public UserDetailsPageModel Build(string userName)
        {
            var currentUser = _auth.CurrentUser;
            var displayUser = _userRepository.GetByNameOrEmail(userName);

            var contextResult = _appContextInteractor.Execute();

            var model = new UserDetailsPageModel
                {
                    BrowserTitle = "User Details",
                    PageProperties = new PageProperties(contextResult),
                    UserName = displayUser.UserName,
                    DisplayName = displayUser.DisplayName,
                    RealName = displayUser.RealName,
                    Email = displayUser.Email,
                    AvatarModel = _avatarModelFactory.Create(displayUser.Email),
                    ShowEditLink = false,
                    ShowPasswordLink = false
                };

            var isViewingCurrentUser = displayUser.UserName == currentUser.UserName;

            if (currentUser.IsAdmin || isViewingCurrentUser)
            {
                model.ShowEditLink = true;
                model.EditUrl = new EditUserUrl(displayUser.UserName);
            }

            if (isViewingCurrentUser)
            {
                model.ShowPasswordLink = true;
                model.ChangePasswordUrl = new ChangePasswordUrl();
            }

            return model;
        }
    }
}