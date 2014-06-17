using Application.Services;
using Application.Urls;
using Core.Repositories;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserDetailsPageBuilder : IUserDetailsPageBuilder
    {
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;

        public UserDetailsPageBuilder(
            IAvatarModelFactory avatarModelFactory, 
            IPagePropertiesFactory pagePropertiesFactory,
            IAuth auth,
            IUserRepository userRepository)
        {
            _avatarModelFactory = avatarModelFactory;
            _pagePropertiesFactory = pagePropertiesFactory;
            _auth = auth;
            _userRepository = userRepository;
        }

        public UserDetailsPageModel Build(string userName)
        {
            var currentUser = _auth.CurrentUser;
            var displayUser = _userRepository.GetByNameOrEmail(userName);
            
            var model = new UserDetailsPageModel
                {
                    BrowserTitle = "User Details",
                    PageProperties = _pagePropertiesFactory.Create(),
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