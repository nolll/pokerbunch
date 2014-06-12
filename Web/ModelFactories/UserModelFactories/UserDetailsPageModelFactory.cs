using Core.Entities;
using Web.ModelFactories.MiscModelFactories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UrlModels;
using Web.Models.UserModels;

namespace Web.ModelFactories.UserModelFactories
{
    public class UserDetailsPageModelFactory : IUserDetailsPageModelFactory
    {
        private readonly IAvatarModelFactory _avatarModelFactory;
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public UserDetailsPageModelFactory(
            IAvatarModelFactory avatarModelFactory, 
            IPagePropertiesFactory pagePropertiesFactory)
        {
            _avatarModelFactory = avatarModelFactory;
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public UserDetailsPageModel Create(User currentUser, User displayUser)
        {
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